using Notes.Data;
using Notes.Hashing;
using Notes.ViewModels;
using System;
using Xamarin.CommunityToolkit.Extensions;
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
            if (App.IsLocked == LockEntity.Undefined)
            {
                ((DisplayPasscodeViewModel)BindingContext).IsChecked = false;
            }
            else
            {
                ((DisplayPasscodeViewModel)BindingContext).IsChecked = true;
            }
        }

        private void ButtonPasscode_Clicked(object sender, EventArgs e)
        {
            string passcode = EntryPassode.Text;

            string md5 = Md5Alghorithm.CreateMD5(passcode);

            ((OnPlatform<string>)Application.Current.Resources["PasscodeMD5"]).Default = md5;

            SettingsData settings = SettingsGroupHandler.GroupSettings();
            var fileHandler = SettingsFileHandler.GetInstance();
            fileHandler.RewriteSettingsFile(settings);
        }

        private bool CheckCorrectness(string first, string second)
        {
            return true;
        }

        private async void TapGestureRecognizerManageQuestion_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SetQuestionPage());
        }
    }
}