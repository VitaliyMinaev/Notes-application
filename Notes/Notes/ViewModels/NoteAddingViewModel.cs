using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Notes.Model;

namespace Notes.ViewModel
{
    public class NoteAddingViewModel : INotifyPropertyChanged
    {
        private Note _note;
        public Note AddingNote 
        { 
            get { return _note; }
            set 
            { 
                _note = value; 
                OnPropertyChanged(); 
            }
        }

        public NoteAddingViewModel()
        {
            AddingNote = new Note();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
