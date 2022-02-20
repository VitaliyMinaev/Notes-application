using System;
using Xamarin.Forms;
using Notes.Data;
using System.IO;

namespace Notes
{
    public partial class App : Application
    {
        // Set the path to data base
        private static NotesDB notesDataBase;
        private static NotesDB basketDataBase;

        public static NotesDB NotesDataBase
        {
            get
            {
                if(notesDataBase == null)
                    notesDataBase = new NotesDB(Path.Combine(Environment.GetFolderPath
                        (Environment.SpecialFolder.LocalApplicationData), "NotesDataBaseV2.db3"));

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

            MainPage = new AppShell();
        }
    }
}
