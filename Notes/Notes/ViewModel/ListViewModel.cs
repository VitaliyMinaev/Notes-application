using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Notes.ViewModel
{
    public class ListViewModel
    {
        public ObservableCollection<string> NotesList { get; set; }

        public ListViewModel()
        {
            NotesList = new ObservableCollection<string> { "Item 1", "Item 2", "Item 3", "Item 4" };
        }
    }
}
