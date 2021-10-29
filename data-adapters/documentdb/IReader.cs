using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAdapters.DocumentDb {
    public interface IReader {
        Task<T?> GetDocument<T>(string collection, object query);

        Task<List<T>> GetDocuments<T>(string collection, object query);
    }
}
