using System;
using Xamarin.Forms;
using Notes.Data;
using System.IO;

namespace Notes
{
    public partial class App : Application
    {
        // Set the path to data base
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
    }
}
