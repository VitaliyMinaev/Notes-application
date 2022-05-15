using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Notes.ViewModels
{
    public class DisplayPasscodeViewModel : INotifyPropertyChanged
    {
        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if(value != _isChecked)
                {
                    _isChecked = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsChecked)));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public DisplayPasscodeViewModel()
        {
            _isChecked = false;
        }
    }
}
