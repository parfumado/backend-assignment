using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Utility.Runtime;

namespace DataAdapters.DocumentDb {
    public class MockDocumentServer : IDocumentAdapter, IWriter, IReader {
        private static Dictionary<string, List<object>> _mockDocumentDb;
        static MockDocumentServer() {
            MockDocumentServer._mockDocumentDb = new Dictionary<string, List<object>>();

            _mockDocumentDb.Add(Collections.User.ToLower().ToString(), new List<object>());
        }

        public Task<List<T>> GetDocuments<T>(string collectionName, object query) {
            List<T> typedResult = new List<T>();

            foreach (object item in this.QueryDocuments(collectionName, query)) {
                typedResult.Add((T)Convert.ChangeType(item, typeof(T)));
            }

            return Task.FromResult<List<T>>(typedResult);
        }

        private IEnumerable<object> QueryDocuments(string collectionName, object query) {
            if (!_mockDocumentDb.ContainsKey(collectionName)) {
                throw new ArgumentException($"Collection '{collectionName}' not found.");
            }

            List<object> collection = _mockDocumentDb[collectionName];

            IEnumerable<object> result = collection.Where(o => {
                foreach (PropertyInfo queryProp in query.GetType().GetProperties()) {
                    bool propertyFound = false;
                    object queryPropValue = queryProp.GetValue(query)!;
                    string queryPropName = queryProp.Name.ToLower();

                    if (queryPropValue == null) {
                        continue;
                    }

                    foreach (PropertyInfo objectProp in o.GetType().GetProperties().Where(p => p.CanRead)) {
                        object objectPropValue = objectProp.GetValue(o)!;
                        string objectPropName = objectProp.Name.ToLower();
                        if (objectPropValue == null)
                            continue;

                        if (objectPropName == queryPropName && object.Equals(objectPropValue, queryPropValue)) {
                            propertyFound = true;
                            break;
                        }
                    }

                    if (!propertyFound) {
                        return false;
                    }
                }

                return true;
            });

            foreach (object item in result) {
                yield return item;
            }
        }

        public Task<T?> GetDocument<T>(string collection, object query) {
            List<T> result = this.GetDocuments<T>(collection, query).Result;

            if (result.Count > 1) {
                throw new Exception("Query returned more than one result. For multiple result records, use GetDocuments instead");
            }

            return Task.FromResult(result.FirstOrDefault());
        }

        public IReader GetReader() {
            return this;
        }

        public IWriter GetWriter() {
            return this;
        }

        public Task<bool> UpdateDocument<T>(string collectionName, object query, T document) where T : notnull, BaseDataModel {
            List<object> queryResult = this.QueryDocuments(collectionName, query).ToList();

            if (queryResult.Count <= 0 || queryResult.Count > 1) {
                throw new InvalidOperationException("Query matches none or multiple documents");
            }

            object result = (object)queryResult.First();
            ObjectUtils.CopyProperties<T, object>(document, true, result);
            document.DateUpdated = DateTime.UtcNow;

            return Task.FromResult(true);
        }

        public Task<bool> InsertDocument<T>(string collectionName, T document) where T : notnull, BaseDataModel {
            if (!_mockDocumentDb.ContainsKey(collectionName)) {
                throw new ArgumentException($"Collection '{collectionName}' not found.");
            }

            document.DateUpdated = DateTime.UtcNow;
            document.DateCreated = DateTime.UtcNow;
            List<object> collection = _mockDocumentDb[collectionName];
            collection.Add(document);

            return Task.FromResult<bool>(true);
        }
    }
}
