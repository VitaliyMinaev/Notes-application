using Notes.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
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

        protected override void OnAppearing()
        {
            int cornerRadius = (int)Application.Current.Resources["CornerRadiusFrame"];

            LabelSlider.Text = $"The value is {cornerRadius}";
            SliderCorner.Value = cornerRadius;
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

        private void RadioButtonSystem_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            string result = (string)((RadioButton)sender).Value;

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

        private async void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            await Task.Run(() =>
            {
                MainThread.BeginInvokeOnMainThread(() => LabelSlider.Text = $"The value is {(int)SliderCorner.Value}");
                Application.Current.Resources["CornerRadiusFrameExample"] = (int)SliderCorner.Value;
            });
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                int value = (int)SliderCorner.Value;

                Application.Current.Resources["CornerRadiusFrame"] = (int)SliderCorner.Value;
                File.WriteAllText(Path.Combine(App.FolderPath, "Settings.txt"), $"CornerRadius:{value}");
            });
        }

        private void RadioButtonFonts_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            string result = (string)((RadioButton)sender).Value;

            if (string.IsNullOrEmpty(result) == true)
                throw new ArgumentException("Doesn't find a radio button with this value");

            if (result == "Default")
                SetDefaultFont();

            SetFont(result);
        }

        private static void SetFont(string fontName)
        {
            Application.Current.Resources["TitleFont"] = fontName;
        }
        private static void SetDefaultFont()
        {
            Application.Current.Resources["TitleFont"] = "";
        }
    }
}