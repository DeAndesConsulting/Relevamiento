using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Relevamiento.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T data);
        Task<bool> UpdateItemAsync(T data);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
