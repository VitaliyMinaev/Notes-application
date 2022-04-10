using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            CheckRadioButton();
        }

        private void CheckRadioButton()
        {
            RadioButton radioButton = GetCurrentThemeRadioButton();

            if (radioButton == null)
                throw new Exception("There are not this theme!");

            radioButton.IsChecked = true;
        }

        private RadioButton GetCurrentThemeRadioButton()
        {
            switch (Settings.Theme)
            {
                case TheTheme.Default:
                    return RadioButtonSystem;
                case TheTheme.Light:
                    return RadioButtonLight;
                case TheTheme.Dark:
                    return RadioButtonDark;
            }

            return null;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var random = new Random();
            var color = Color.FromRgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));

            App.Current.Resources["FrameColor"] = color;
        }

        private void RadioButtonSystem_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            string result = ((RadioButton)sender).Value as string;
            if (string.IsNullOrEmpty(result) == true)
                throw new ArgumentException("Doesn't find a radio button with this value");

            SetThemeInSettings(result);

            XamarinTheme.SetTheme();
        }

        private static void SetThemeInSettings(string result)
        {
            switch (result)
            {
                case "System":
                    Settings.Theme = TheTheme.Default;
                    break;
                case "Light":
                    Settings.Theme = TheTheme.Light;
                    break;
                case "Dark":
                    Settings.Theme = TheTheme.Dark;
                    break;
            }
        }
    }
}