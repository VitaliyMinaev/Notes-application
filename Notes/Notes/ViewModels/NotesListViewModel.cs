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
            Notes = new ObservableCollection<Note>(GetNotes());

            MessagingCenter.Subscribe<NoteAddingPage, Note>(this, nameof(NoteAddingPage), (page, note) =>
            {
                if(note.NoteId == -1)
                {
                    // if we add note
                    note.NoteId = App.NotesDataBase.GetLastIndexAsync() + 1;
                    App.NotesDataBase.AddAsync(note);

                    Notes.Add(note);
                }
                else
                {
                    // If we change note
                    Note noteToEdit = Notes.Where(n => n.NoteId == note.NoteId).FirstOrDefault();

                    int newIndex = Notes.IndexOf(noteToEdit);
                    Notes.Remove(noteToEdit);

                    Notes.Add(note);

                    int oldIndex = Notes.IndexOf(note);

                    Notes.Move(oldIndex, newIndex);

                    App.NotesDataBase.UpdateAsync(note);
                }
            });
        }

        private List<Note> GetNotes()
        {
            var taskList = App.NotesDataBase.GetNotesAsync();

            return taskList.Result;
        }

        public void AddNote()
        {
            Notes.Add(AddingNote);
        }
        public void RemoveNote() => Notes.Remove(SelectedNote);
    }
}
