using System.Threading.Tasks;

namespace DataAdapters.KeyValueDb {
    public interface IReader {
        public Task<T?> GetKey<T>(string key);
    }
}
