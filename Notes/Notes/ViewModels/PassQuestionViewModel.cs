using Notes.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Notes.ViewModels
{
    public class PassQuestionViewModel : INotifyPropertyChanged
    {
        private QuestionEntity _questionEntity;
        public QuestionEntity QuestionEntity
        {
            get
            {
                _questionEntity.QuestionText = ((OnPlatform<string>)Application.Current.Resources["Question"]).Default;
                _questionEntity.AnswerText = ((OnPlatform<string>)Application.Current.Resources["Answer"]).Default;

                return _questionEntity;
            }
            set
            {
                if(value != _questionEntity)
                {
                    _questionEntity = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QuestionEntity)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public PassQuestionViewModel()
        {
            _questionEntity = new QuestionEntity();
        }
    }
}
