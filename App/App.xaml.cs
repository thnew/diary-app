using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App.Services;
using App.Views;
using App.Models;

namespace App
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<IDataStore<DiaryEntry>, DiaryEntryDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
