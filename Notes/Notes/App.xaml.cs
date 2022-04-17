using System;
using Xamarin.Forms;
using Notes.Data;
using System.IO;
using Notes.Views;
using Notes.Models;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Linq;

[assembly: ExportFont("SquarePeg-Regular.ttf", Alias = "SquarePeg"), ExportFont("Akshar-Regular.ttf", Alias = "AksharReg"), 
    ExportFont("LibreBaskerville-Regular.ttf", Alias = "LibreBaskerville"), ExportFont("Pacifico-Regular.ttf", Alias = "Pacifico"),
    ExportFont("PTSerif-Regular.ttf", Alias = "PTFerif"), ExportFont("RobotoSlab-VariableFont_wght.ttf", Alias = "RobotoSlab")]
namespace Notes
{
    public partial class App : Application
    {
        // Set the path to data base
        private static NotesDB notesDataBase;
        private static NotesDB basketDataBase;
        private static string folderPath;
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
        public static string FolderPath
        {
            get => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

        public App()
        {
            InitializeComponent();

            CheckExistingFileOrCreateNewAsync();
            SetThemeAsync();

            basket = new BasketPage();
            MainPage = new AppShell();
        }

        private static async void SetThemeAsync()
        {
            await Task.Run(() => XamarinTheme.SetTheme());
        }

        private static async void CheckExistingFileOrCreateNewAsync()
        {
            await Task.Run(() =>
            {
                if (File.Exists(Path.Combine(FolderPath, "Settings.txt")) == false)
                    File.Create(Path.Combine(FolderPath, "Settings.txt"));
            });
        }

        private async void SetSettingsAsync()
        {
            await Task.Run(() =>
            {
                int value = ReadCornerRadiusFromSettings();
                if (value == -1)
                {
                    Application.Current.Resources["CornerRadiusFrame"] = 30;
                }
                else
                {
                    Application.Current.Resources["CornerRadiusFrame"] = value;
                }
            });
        }
        private int ReadCornerRadiusFromSettings()
        {
            string result = File.ReadAllText(Path.Combine(FolderPath, "Settings.txt"));

            if (string.IsNullOrEmpty(result) == true)
                return -1;

            var list = result.Split(':').ToList();
            int value;

            if (int.TryParse(list[1], out value) == false)
                throw new ArgumentException("In settings not a value");

            return value;
        }

        protected override void OnStart()
        {
            SetSettingsAsync();
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
