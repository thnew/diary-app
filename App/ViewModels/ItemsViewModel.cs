using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using App.Models;
using App.Views;

namespace App.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private DiaryEntry _selectedItem;
        public DiaryEntry SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private bool _hasErrors;
        public bool HasErrors
        {
            get => _hasErrors;
            set => SetProperty(ref _hasErrors, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }


        public ObservableCollection<DiaryEntry> Items { get; }

        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<DiaryEntry> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Diary";
            Items = new ObservableCollection<DiaryEntry>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<DiaryEntry>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            HasErrors = false;

            try
            {
                Items.Clear();

                var result = await DataStore.GetItemsAsync(true);

                HasErrors = result.hasErrors;
                ErrorMessage = result.errorMessage;

                if (!_hasErrors)
                {
                    foreach (var item in result.entries)
                    {
                        Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(DiaryEntry item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}