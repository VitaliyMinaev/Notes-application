using Notes.Model;
using Notes.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Notes.ViewModel
{
    public class NotesListViewModel
    {
        public ObservableCollection<Note> Notes { get; set; }

        public Note AddingNote { get; set; }
        public Note SelectedNote { get; set; }

        public ICommand AddNoteCommand => new Command(AddNote);
        public ICommand RemoveNoteCommand => new Command(RemoveNote);

        public NotesListViewModel()
        {
            Notes = new ObservableCollection<Note>();

            Notes.Add(new Note("Item 1", "Item 1 text", DateTime.Now));
            Notes.Add(new Note("Item 2", "Item 2 text", DateTime.Now));
            Notes.Add(new Note ("Item 3", "Item 3 text", DateTime.Now));

            MessagingCenter.Subscribe<NoteAddingPage, Note>(this, nameof(NoteAddingPage), (page, note) =>
            {
                if(note.NoteId == -1)
                {
                    note.NoteId = Notes.Count;
                    Notes.Add(note);
                }
                else
                {
                    Note noteToEdit = Notes.Where(n => n.NoteId == note.NoteId).FirstOrDefault();

                    int newIndex = Notes.IndexOf(noteToEdit);
                    Notes.Remove(noteToEdit);

                    Notes.Add(note);

                    int oldIndex = Notes.IndexOf(note);

                    Notes.Move(oldIndex, newIndex);
                }
            });
        }

        public void AddNote() => Notes.Add(AddingNote);
        public void RemoveNote() => Notes.Remove(SelectedNote);
    }
}
