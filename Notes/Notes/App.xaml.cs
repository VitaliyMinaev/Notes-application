using System;
using Xamarin.Forms;
using Notes.Data;
using System.IO;
using Notes.Views;
using Notes.ViewModel;
using Notes.Models;
using Xamarin.Essentials;

namespace Notes
{
    public partial class App : Application
    {
        // Set the path to data base
        private static NotesDB notesDataBase;
        private static NotesDB basketDataBase;
        private BasketPage basket;
        public static NotesDB NotesDataBase
        {
            get
            {
                if(notesDataBase == null)
                    notesDataBase = new NotesDB(Path.Combine(Environment.GetFolderPath
                        (Environment.SpecialFolder.LocalApplicationData), "NotesDataBase.db3"));

                return notesDataBase;
            }
        }
        public static NotesDB BasketDataBase
        {
            get
            {
                if (basketDataBase == null)
                    basketDataBase = new NotesDB(Path.Combine(Environment.GetFolderPath
                        (Environment.SpecialFolder.LocalApplicationData), "BasketDB.db3"));

                return basketDataBase;
            }
        }

        public App()
        {
            InitializeComponent();

            XamarinTheme.SetTheme();

            basket = new BasketPage();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            SetThemeAndAddHandler();
        }

        protected override void OnSleep()
        {
            SetThemeAndRemoveHandler();
        }

        private void SetThemeAndRemoveHandler()
        {
            XamarinTheme.SetTheme();
            RequestedThemeChanged -= App_RequestedThemeChanged;
        }

        protected override void OnResume()
        {
            SetThemeAndAddHandler();
        }

        private void SetThemeAndAddHandler()
        {
            XamarinTheme.SetTheme();
            RequestedThemeChanged += App_RequestedThemeChanged;
        }

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                XamarinTheme.SetTheme();
            });
        }
    }
}
