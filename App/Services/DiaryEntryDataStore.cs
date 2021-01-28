using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using App.Models;
using System.Net.Http.Json;
using System;

namespace App.Services
{
    public class DiaryEntryDataStore : IDataStore<DiaryEntry>
    {
        private List<DiaryEntry> _items;

        public async Task<bool> AddItemAsync(DiaryEntry item)
        {
            _items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(DiaryEntry item)
        {
            var oldItem = _items.Where((DiaryEntry arg) => arg.Id == item.Id).FirstOrDefault();
            _items.Remove(oldItem);
            _items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(long id)
        {
            var oldItem = _items.Where((DiaryEntry arg) => arg.Id == id).FirstOrDefault();
            _items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<DiaryEntry> GetItemAsync(long id)
        {
            return await Task.FromResult(_items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<(bool hasErrors, string errorMessage, IEnumerable<DiaryEntry> entries)> GetItemsAsync(bool forceRefresh = false)
        {
            if (_items == null)
            {
                var result = await GetAll();
                if (result.hasErrors)
                {
                    return result;
                }

                _items = result.entries.ToList();
            }

            return (false, null, _items);
        }

        private async Task<(bool hasErrors, string errorMessage, IEnumerable<DiaryEntry> entries)> GetAll()
        {
            try
            {
                var client = new HttpClient();
                return (false, null, await client.GetFromJsonAsync<IEnumerable<DiaryEntry>>("https://localhost:5001/api/diary"));
            }
            catch(Exception e)
            {
                return (true, e.Message, null);
            }
        }
    }
}