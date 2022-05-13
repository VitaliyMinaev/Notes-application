using Notes.ViewModels;
using System;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LockPage : ContentPage
    {
        public string Passcode { get; set; }
        public LockPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void ButtonNumber_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string commandParameter = (string)button.CommandParameter;

            ((PasscodeViewModel)BindingContext).Passcode += commandParameter;

            await CheckPasscode();
        }

        private async Task CheckPasscode()
        {
            await Task.Run(() =>
            {
                if (IsCorrect() == true)
                    Navigation.PopAsync();
            });
        }

        private bool IsCorrect()
        {
            if (((PasscodeViewModel)BindingContext).Passcode == "7518")
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
    }
}