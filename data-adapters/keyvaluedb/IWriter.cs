using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAdapters.KeyValueDb {
    public interface IWriter {
        public Task<bool> SetKey<T>(string key, T value, TimeSpan? ttl = null);

        public Task<bool> SetKeyTtl(string key, TimeSpan ttl);

        public Task<bool> ResetKeyTtl(string key);

        public Task<bool> DeleteKey(string key);
    }
}
