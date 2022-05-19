using Notes.Hashing;
using Notes.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PassControlQuestionPage : ContentPage
    {
        public PassControlQuestionPage()
        {
            InitializeComponent();
        }

        private void ButtonConfirm_Clicked(object sender, EventArgs e)
        {
            string userAnswer = EntryAnswer.Text;
            string hash = Md5Alghorithm.CreateMD5(userAnswer);
            string correctAnswer = ((PassQuestionViewModel)BindingContext).QuestionEntity.AnswerText;

            if (hash == correctAnswer)
            {
                MessagingCenter.Send(this, nameof(PassControlQuestionPage), true);
            }
            else
            {
                FrameEntry.BorderColor = Color.Red;
            }
        }
    }
}