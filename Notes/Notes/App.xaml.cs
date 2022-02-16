using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Notes.Data;
using System.IO;

namespace Notes
{
    public partial class App : Application
    {
        private static NotesDB notesDataBase = NotesDB.Initialize(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NotesDataBaseV2.db3"));

        public static NotesDB NotesDataBase
        {
            get => notesDataBase;
        }

        public App()
        {
            InitializeComponent();

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
