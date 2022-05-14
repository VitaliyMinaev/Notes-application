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
        }

        private void ButtonNumber_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string commandParameter = (string)button.CommandParameter;

            ((PasscodeViewModel)BindingContext).Passcode += commandParameter;

            CheckPasscodeAsync();
        }

        private async void CheckPasscodeAsync()
        {
            bool isCorrect = IsCorrectAsync();
            if (isCorrect == true)
            {
                App.IsLocked = false;
                await Navigation.PopAsync();
            }
        }

        private bool IsCorrectAsync()
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