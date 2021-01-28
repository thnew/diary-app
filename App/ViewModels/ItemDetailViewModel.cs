using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace App.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private long _itemId;
        private string _description;
        private DateTime _eventAt;
        public long Id { get; set; }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public DateTime EventAt
        {
            get => _eventAt;
            set => SetProperty(ref _eventAt, value);
        }

        public string ItemId
        {
            set
            {
                _itemId = long.Parse(value);
                LoadItemId(_itemId);
            }
        }

        public async void LoadItemId(long itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                EventAt = item.EventAt;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
