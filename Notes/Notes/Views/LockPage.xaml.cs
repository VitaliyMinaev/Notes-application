using Notes.Data;
using Notes.Hashing;
using Notes.ViewModels;
using System;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LockPage : ContentPage
    {
        public LockPage()
        {
            InitializeComponent();
        }

        private void ButtonNumber_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string commandParameter = (string)button.CommandParameter;

            ((PasscodeViewModel)BindingContext).Passcode += commandParameter;
        }

        private async void CheckPasscodeAsync()
        {
            bool isCorrect = IsCorrectAsync();

            if (isCorrect == true)
            {
                await SetNewSettings();
                await Navigation.PopAsync();
            }
        }

        private static async Task SetNewSettings()
        {
            await Task.Run(() =>
            {
                App.IsLocked = Data.LockEntity.Unlocked;

                SettingsData settings = SettingsGroupHandler.GroupSettings();
                var fileHandler = SettingsFileHandler.GetInstance();
                fileHandler.RewriteSettingsFile(settings);
            });
        }

        private bool IsCorrectAsync()
        {
            string realPasscode = ((OnPlatform<string>)Application.Current.Resources["PasscodeMD5"]).Default;
            string passcode = Md5Alghorithm.CreateMD5(((PasscodeViewModel)BindingContext).Passcode);

            if (passcode == realPasscode)
                return true;

            return false;
        }

        private async void ButtonRemove_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(((PasscodeViewModel)BindingContext).Passcode) == true)
                return;

            await Task.Run(() =>
            {
                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < EntryPasscode.Text.Length - 1; i++)
                {
                    stringBuilder.Append(EntryPasscode.Text[i]);
                }

                ((PasscodeViewModel)BindingContext).Passcode = stringBuilder.ToString();
            });
        }

        private void ButtonEnter_Clicked(object sender, EventArgs e)
        {
            CheckPasscodeAsync();
        }
    }
}