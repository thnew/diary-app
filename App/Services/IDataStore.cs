using System.Collections.Generic;
using System.Threading.Tasks;
using App.Models;

namespace App.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(long id);
        Task<T> GetItemAsync(long id);
        Task<(bool hasErrors, string errorMessage, IEnumerable<DiaryEntry> entries)> GetItemsAsync(bool forceRefresh = false);
    }
}
