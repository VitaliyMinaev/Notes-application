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

        private void Button_Clicked(object sender, EventArgs e)
        {
            ((DisplayPasscodeViewModel)BindingContext).IsChecked = !((DisplayPasscodeViewModel)BindingContext).IsChecked;
        }
    }
}