using System;
using Xamarin.Forms;
using Notes.Data;
using System.IO;
using Notes.Views;
using Notes.Models;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

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
        private BasketPage basket;
        public static NotesDB NotesDataBase
        {
            get
            {
                if (notesDataBase == null)
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
        public static LockEntity IsLocked { get; set; }

        public App()
        {
            InitializeComponent();

            basket = new BasketPage();
            MainPage = new AppShell();

            IsLocked = LockEntity.Undefined;

            CheckExistingFileOrCreateNewAsync();
            SetSettingsAsync();
            SetThemeAsync();
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

                string dataFromSettings = File.ReadAllText(Path.Combine(FolderPath, "Settings.txt"));

                if (string.IsNullOrEmpty(dataFromSettings) == true)
                    SetDefaultSettings();
            });
        }
        private async void SetSettingsAsync()
        {
            await Task.Run(() =>
            {
                SettingsData settings = GetUserSettings();

                if (settings != null)
                {
                    int cornerRadius = settings.CornerRadius;
                    SetCornerRadius(cornerRadius);
                    SetTitleFont(settings.Fonts.TitleFont);
                    SetDateFont(settings.Fonts.DateFont);
                    SetIsLocked(settings.Locked);
                    SetPasscode(settings.Passcode);
                }
            });
        }

        private static void SetDefaultSettings()
        {
            var settingsHandler = SettingsFileHandler.GetInstance();
            settingsHandler.ClearSettingsFile();
        }

        private void SetCornerRadius(int cornerRadius)
        {
            if (cornerRadius == -1)
                Application.Current.Resources["CornerRadiusFrame"] = 30;
            else
                Application.Current.Resources["CornerRadiusFrame"] = cornerRadius;
        }
        private void SetTitleFont(string font)
        {
            if (font != null && font != "Default")
                Application.Current.Resources["TitleFont"] = font;
        }
        private void SetDateFont(string font)
        {
            if(font != null && font != "Default")
                Application.Current.Resources["DateFont"] = font;
        }
        private void SetIsLocked(LockEntity locked)
        {
            IsLocked = locked;
        }
        private void SetPasscode(string passcode)
        {
            if(string.IsNullOrEmpty(passcode) == false)
                ((OnPlatform<string>)Application.Current.Resources["PasscodeMD5"]).Default = passcode;
        }

        private SettingsData GetUserSettings()
        {
            string dataFromSettings = File.ReadAllText(Path.Combine(FolderPath, "Settings.txt"));

            if (string.IsNullOrEmpty(dataFromSettings) == true)
                return null;

            return FillDataInVariable(dataFromSettings);
        }
        private SettingsData FillDataInVariable(string dataFromSettings)
        {
            var settingsData = new SettingsData();
            Dictionary<string, string> dictionary = SplitValues(dataFromSettings);

            foreach (var item in dictionary)
            {
                string parameter = item.Key, value = item.Value;

                switch (parameter)
                {
                    case "CornerRadius":
                        int corerRadius = ParseFromString(value);
                        settingsData.CornerRadius = corerRadius;
                        break;
                    case "TitleFont":
                        settingsData.Fonts.TitleFont = value;
                        break;
                    case "DateFont":
                        settingsData.Fonts.DateFont = value;
                        break;
                    case "IsLocked":
                        settingsData.Locked = GetLockEntity(value);
                        break;
                    case "Passcode":
                        settingsData.Passcode = value;
                        break;
                }
            }

            return settingsData;
        }
        private static Dictionary<string, string> SplitValues(string dataFromSettings)
        {
            var list = dataFromSettings.Split(' ').ToList();
            var dictionary = new Dictionary<string, string>();

            foreach(var item in list)
            {
                var pair = item.Split(':').ToList();
                dictionary.Add(pair[0], pair[1]);
            }

            return dictionary;
        }

        private LockEntity GetLockEntity(string value)
        {
            switch (value)
            {
                case "Undefined":
                    return LockEntity.Undefined;
                case "Locked":
                    return LockEntity.Locked;
                case "Unlocked":
                    return LockEntity.Unlocked;
            }

            throw new ArgumentException($"LockEntity does'n has this({value}) value");
        }
        private int ParseFromString(string value)
        {
            int result;

            if (int.TryParse(value, out result) == false)
                throw new ArgumentException("In corner radius not an integer");

            return result;
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
