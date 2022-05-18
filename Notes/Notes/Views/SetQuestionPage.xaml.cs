using Notes.Data;
using Notes.Hashing;
using Notes.ViewModels;
using System;
using System.Threading.Tasks;
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
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string answer = ((AnswerQuestionViewModel)BindingContext).Answer;
            var hashAnswer = Md5Alghorithm.CreateMD5(answer);

            var questionEntity = new QuestionEntity(labelQuestion.Text, hashAnswer);

            WriteQuestionInSettings(questionEntity);

            await Navigation.PopAsync();
        }

        private async void WriteQuestionInSettings(QuestionEntity answer)
        {
            await Task.Run(() =>
            {
                ((OnPlatform<string>)Application.Current.Resources["Question"]).Default = answer.QuestionText;
                ((OnPlatform<string>)Application.Current.Resources["Answer"]).Default = answer.AnswerText;

                SettingsData settings = SettingsGroupHandler.GroupSettings(answer);

                var fileHandler = SettingsFileHandler.GetInstance();
                fileHandler.RewriteSettingsFile(settings);
            });
        }
    }
}