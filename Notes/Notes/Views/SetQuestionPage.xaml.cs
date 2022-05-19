using Notes.Data;
using Notes.Hashing;
using Notes.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetQuestionPage : ContentPage
    {
        public SetQuestionPage()
        {
            InitializeComponent();
            ShowOrHideControls();
        }

        private void ShowOrHideControls()
        {
            if (((OnPlatform<string>)Application.Current.Resources["Answer"]).Default == "None")
            {
                StackDoesntHaveQuestion.IsVisible = true;
                StackHasQuestion.IsVisible = false;
            }
            else
            {
                StackDoesntHaveQuestion.IsVisible = false;
                StackHasQuestion.IsVisible = true;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string answer = ((AnswerQuestionViewModel)BindingContext).Answer;
            var hashAnswer = Md5Alghorithm.CreateMD5(answer);

            var questionEntity = new QuestionEntity(labelQuestion.Text, hashAnswer);

            WriteQuestionInSettings(questionEntity);

            await Navigation.PopAsync();
        }

        private void WriteQuestionInSettings(QuestionEntity answer)
        {
            ((OnPlatform<string>)Application.Current.Resources["Question"]).Default = answer.QuestionText;
            ((OnPlatform<string>)Application.Current.Resources["Answer"]).Default = answer.AnswerText;

            SettingsData settings = SettingsGroupHandler.GroupSettings(answer);

            var fileHandler = SettingsFileHandler.GetInstance();
            fileHandler.RewriteSettingsFile(settings);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ((OnPlatform<string>)Application.Current.Resources["Question"]).Default = "None";
            ((OnPlatform<string>)Application.Current.Resources["Answer"]).Default = "None";

            ShowOrHideControls();

            SettingsData settings = SettingsGroupHandler.GroupSettings();
            var fileHandler = SettingsFileHandler.GetInstance();
            fileHandler.RewriteSettingsFile(settings);
        }
    }
}