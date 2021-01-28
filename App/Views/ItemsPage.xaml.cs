using Xamarin.Forms;
using App.ViewModels;
using System.ComponentModel;

namespace App.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();

            _viewModel.PropertyChanged += OnErrorMessage;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void OnErrorMessage(object value, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != nameof(ItemsViewModel.HasErrors)
                && args.PropertyName != nameof(ItemsViewModel.ErrorMessage)) return;
            if (!(_viewModel?.HasErrors ?? false)
                || string.IsNullOrWhiteSpace(_viewModel.ErrorMessage)) return;

            DisplayAlert("Something went wrong", _viewModel.ErrorMessage, "Ok");
        }
    }
}