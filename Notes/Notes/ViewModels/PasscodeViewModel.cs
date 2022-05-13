using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Notes.ViewModels
{
    public class PasscodeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _passcode;
        public string Passcode
        {
            get => _passcode;
            set
            {
                if(_passcode != value)
                {
                    _passcode = value;

                    OnPropertyChanged(nameof(Passcode));
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PasscodeViewModel()
        {
            _passcode = "";
        }
    }
}
