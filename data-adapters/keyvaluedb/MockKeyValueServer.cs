using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAdapters.KeyValueDb {
    public class MockKeyValueServer : IKeyValueAdapter, IWriter, IReader {
        private DictionaryIndex _readerDb;
        private DictionaryIndex _writerDb;
        static Dictionary<string, (string value, DateTime? expiration)> _mockCache = new Dictionary<string, (string value, DateTime? expiration)>();

        public Task<T?> GetKey<T>(string key) {
            (string value, DateTime? expiration)? cacheObject = _mockCache.ContainsKey(key) ? _mockCache[key] : null;

            if (!cacheObject.HasValue || cacheObject == default) {
                return Task.FromResult<T?>(default);
            }

            if (cacheObject.Value.expiration.HasValue && cacheObject.Value.expiration.Value < DateTime.UtcNow) {
                _mockCache.Remove(key);
                return Task.FromResult<T?>(default);
            }

            return Task.FromResult<T?>(JsonSerializer.Deserialize<T>(cacheObject.Value.value));
        }

        public IReader GetReader(DictionaryIndex dbIndex) {
            this._readerDb = dbIndex;
            return this;
        }

        public IWriter GetWriter(DictionaryIndex dbIndex) {
            this._writerDb = dbIndex;
            return this;
        }

        public Task<bool> SetKey<T>(string key, T value, TimeSpan? ttl) {
            if (value is null) {
                return Task.FromResult<bool>(false);
            }

            _mockCache[key] = (JsonSerializer.Serialize(value), ttl.HasValue ? DateTime.UtcNow.Add(ttl.Value) : null);
            return Task.FromResult<bool>(true);
        }

        public Task<bool> SetKeyTtl(string key, TimeSpan ttl) {
            if (!_mockCache.ContainsKey(key) || (_mockCache[key].expiration.HasValue && _mockCache[key].expiration!.Value < DateTime.UtcNow))
                return Task.FromResult<bool>(false);

            _mockCache[key] = (_mockCache[key].value, DateTime.UtcNow.Add(ttl));
            return Task.FromResult<bool>(true);
        }


        public Task<bool> ResetKeyTtl(string key) {
            TimeSpan? ttl = DefaultExpirationAttribute.GetDefaultExpiration(this._writerDb);
            if (!ttl.HasValue) {
                throw new InvalidOperationException("Database Index has no default expiration set");
            }

            return SetKeyTtl(key, ttl.Value);
        }

        public Task<bool> DeleteKey(string key) {
            return Task.FromResult<bool>(_mockCache.Remove(key));
        }
    }
}