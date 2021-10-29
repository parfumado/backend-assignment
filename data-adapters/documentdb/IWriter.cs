using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAdapters.DocumentDb {
    public interface IWriter {
        Task<bool> InsertDocument<T>(string collectionName, T document) where T : notnull, BaseDataModel;

        Task<bool> UpdateDocument<T>(string collectionName, object query, T document) where T : notnull, BaseDataModel;
    }
}
