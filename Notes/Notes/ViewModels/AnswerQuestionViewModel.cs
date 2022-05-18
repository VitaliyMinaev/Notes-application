using System.ComponentModel;

namespace Notes.ViewModels
{
    public class AnswerQuestionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _answer;
        public AnswerQuestionViewModel()
        {
            _answer = "";
        }

        public string Answer
        {
            get => _answer;
            set
            {
                if(value != _answer)
                {
                    _answer = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer)));
                }
            }
        }
    }
}
