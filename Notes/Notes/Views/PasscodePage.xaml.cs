using Notes.Data;
using Notes.Hashing;
using Notes.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasscodePage : ContentPage
    {
        public PasscodePage()
        {
            InitializeComponent();
            ExistPasscode();
        }

        private void ExistPasscode()
        {
            if (((OnPlatform<string>)Application.Current.Resources["PasscodeMD5"]).Default == "None")
            {
                ((DisplayPasscodeViewModel)BindingContext).IsChecked = false;
            }
            else
            {
                ((DisplayPasscodeViewModel)BindingContext).IsChecked = true;
            }
        }

        private void ButtonPasscodeSave_Clicked(object sender, EventArgs e)
        {
            string passcode = EntryPassode.Text;
            string passcodeConfirm = ConfirmPasscodeEntry.Text;

            if(passcode == passcodeConfirm)
            {
                ((DisplayPasscodeViewModel)BindingContext).IsChecked = true;

                string md5 = Md5Alghorithm.CreateMD5(passcode);

                ((OnPlatform<string>)Application.Current.Resources["PasscodeMD5"]).Default = md5;

                SettingsData settings = SettingsGroupHandler.GroupSettings();
                var fileHandler = SettingsFileHandler.GetInstance();
                fileHandler.RewriteSettingsFile(settings);
            }
            else
            {
                ConfirmPasscodeFrame.BorderColor = Color.Red;
            }
        }

        private bool CheckCorrectness(string first, string second)
        {
            return true;
        }

        private async void TapGestureRecognizerManageQuestion_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SetQuestionPage());
        }

        private async void ButtonPasscodeChange_Clicked(object sender, EventArgs e)
        {
            string realPasscode = ((OnPlatform<string>)Application.Current.Resources["PasscodeMD5"]).Default;
            string input = EntryOldPasscode.Text;
            string inputPasscode = Md5Alghorithm.CreateMD5(input);

            if(realPasscode == inputPasscode)
            {
                FrameOldPasscode.BorderColor = Color.Green;

                if(EntryPasscodeNew.Text == EntryConfirmPasscodeNew.Text)
                {
                    FrameNewPasscode.BorderColor = Color.Green;
                    FrameConfirmNewPasscode.BorderColor = Color.Green;

                    string newPasscode = Md5Alghorithm.CreateMD5(EntryPasscodeNew.Text);
                    ((OnPlatform<string>)Application.Current.Resources["PasscodeMD5"]).Default = newPasscode;

                    SettingsData settings = SettingsGroupHandler.GroupSettings();
                    var fileHandler = SettingsFileHandler.GetInstance();
                    fileHandler.RewriteSettingsFile(settings);

                    await DisplayAlert("Passcode changing", "You have changed your passcode", "Close");
                }
                else
                {
                    FrameConfirmNewPasscode.BorderColor = Color.Red;
                }
            }
            else
            {
                FrameOldPasscode.BorderColor = Color.Red;
            }
        }

        private async void TapGestureRecognizer_TappedTurnOffPasscode(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Turn off passcode", "Are you sure?", "Yes", "No");

            if (result == false)
                return;

            ((OnPlatform<string>)Application.Current.Resources["PasscodeMD5"]).Default = "None";

            ((DisplayPasscodeViewModel)BindingContext).IsChecked = false;

            SettingsData settings = SettingsGroupHandler.GroupSettings();
            var fileHandler = SettingsFileHandler.GetInstance();
            fileHandler.RewriteSettingsFile(settings);
        }
    }
}