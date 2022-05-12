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

            CheckRadioButtonsAsync();
        }

        protected override void OnAppearing()
        {
            int cornerRadius = (int)Application.Current.Resources["CornerRadiusFrame"];

            LabelSlider.Text = $"The value is {cornerRadius}";
            SliderCorner.Value = cornerRadius;
        }

        private void CheckRadioButtonsAsync()
        {
            CheckThemesRadioButtons();
            CheckFontsRadioButtons();
        }

        private void CheckFontsRadioButtons()
        {
            string titleFontName = Application.Current.Resources["TitleFont"].ToString();
            string dateFontName = Application.Current.Resources["DateFont"].ToString();

            RadioButton title = GetCurrentTitleFontRadioBurron(titleFontName, "Title");
            CheckRadioButton(title, titleFontName);

            RadioButton date = GetCurrentTitleFontRadioBurron(dateFontName, "Date");
            CheckRadioButton(date, dateFontName);
        }

        private void CheckRadioButton(RadioButton toCheck, string titleFontName)
        {
            if (toCheck == null)
                return;
            MainThread.BeginInvokeOnMainThread(() => toCheck.IsChecked = true);
        }

        private void CheckThemesRadioButtons()
        {
            RadioButton radioButton = GetCurrentThemeRadioButton();

            if (radioButton == null)
                throw new Exception("There are not this theme!");

            MainThread.BeginInvokeOnMainThread(() => radioButton.IsChecked = true);
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
        private RadioButton GetCurrentTitleFontRadioBurron(string fontName, string titleOrDate)
        {
            switch (fontName)
            {
                case "Default":
                    return titleOrDate == "Title" ? DefaultRb : DDefaultRb;
                case "SquarePeg-Regular.ttf":
                    return titleOrDate == "Title" ? SPRb : DSPRb;
                case "Akshar-Regular.ttf":
                    return titleOrDate == "Title" ? ARb : DARb;
                case "LibreBaskerville-Regular.ttf":
                    return titleOrDate == "Title" ? LBRb : DLBRb;
                case "PTSerif-Regular.ttf":
                    return titleOrDate == "Title" ? PTSRb : DPTSRb;
                case "RobotoSlab-VariableFont_wght.ttf":
                    return titleOrDate == "Title" ? RSRb : DRSRb;
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
                RewriteSettingsInFile();
            });
        }

        private void RadioButtonFontsTitle_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            string result = (string)((RadioButton)sender).Value;

            if (string.IsNullOrEmpty(result) == true)
                throw new ArgumentException("Doesn't find a radio button with this value");

            if (result == "Default")
                SetDefaultTitleFont();

            SetTitleFont(result);

            // await Task.Run(() => RewriteSettingsInFile());
            RewriteSettingsInFile();
        }
        private static void SetTitleFont(string fontName)
        {
            Application.Current.Resources["TitleFont"] = fontName;
        }
        private static void SetDefaultTitleFont()
        {
            Application.Current.Resources["TitleFont"] = "";
        }

        private void RadioButtonFontsDate_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            string result = (string)((RadioButton)sender).Value;

            if (string.IsNullOrEmpty(result) == true)
                throw new ArgumentException("Doesn't find a radio button with this value");
            if (result == "Default")
                SetDefaultDateFont();

            SetDateFont(result);
            // await Task.Run(() => RewriteSettingsInFile());
            RewriteSettingsInFile();
        }
        private static void SetDateFont(string fontName)
        {
            Application.Current.Resources["DateFont"] = fontName;
        }
        private static void SetDefaultDateFont()
        {
            Application.Current.Resources["DateFont"] = "";
        }

        private static void RewriteSettingsInFile()
        {
            string dataInFile = $"CornerRadius:{Application.Current.Resources["CornerRadiusFrame"]} " +
                                    $"TitleFont:{Application.Current.Resources["TitleFont"]} " +
                                    $"DateFont:{Application.Current.Resources["DateFont"]}";

            File.WriteAllText(Path.Combine(App.FolderPath, "Settings.txt"), dataInFile);
        }
    }
}