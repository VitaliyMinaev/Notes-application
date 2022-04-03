using Notes.Model;
using Notes.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Notes.ViewModel
{
    public class NotesListViewModel
    {
        public ObservableCollection<Note> Notes { get; set; }

        public Note AddingNote { get; set; }

        public NotesListViewModel()
        {
            List<Note> notes = GetNotesFromDataBase();
            Notes = new ObservableCollection<Note>(notes);

            SubscribeToMessageCenter();
        }

        private List<Note> GetNotesFromDataBase()
        {
            var taskList = App.NotesDataBase.GetNotesAsync();

            return taskList.Result;
        }

        private void SubscribeToMessageCenter()
        {
            MessagingCenter.Subscribe<NotesPage>(this, nameof(NotesPage), (page) =>
            {
                foreach (var colorNote in Notes)
                    colorNote.Color = Color.FromRgb(colorNote.R, colorNote.G, colorNote.B);
            });

            MessagingCenter.Subscribe<NotesPage, Note>(this, nameof(NotesListViewModel), (page, note) =>
            {
                ChangeNote(note);
            });

            MessagingCenter.Subscribe<NoteAddingPage, Note>(this, nameof(NoteAddingPage), (page, note) =>
            {
                if (note.NoteId == -1)
                    AddNote(note);
                else
                    ChangeNote(note);
            });

            MessagingCenter.Subscribe<BasketPage, Note>(this, nameof(BasketPage), (page, note) =>
            {
                 AddNote(note);
            });
        }

        private void AddNote(Note note)
        {
            note.NoteId = App.NotesDataBase.GetLastIndexAsync() + 1;
            App.NotesDataBase.AddAsync(note);

            Notes.Add(note);
        }

        private void ChangeNote(Note note)
        {
            // Select note which we need to change
            Note noteToEdit = Notes.Where(n => n.NoteId == note.NoteId).FirstOrDefault();

            // Get old note's index
            int newIndex = Notes.IndexOf(noteToEdit);
            Notes.Remove(noteToEdit);

            // Add already changed note
            Notes.Add(note);

            int oldIndex = Notes.IndexOf(note);

            // Insert note into old note's place
            Notes.Move(oldIndex, newIndex);

            // Add note into data base
            App.NotesDataBase.UpdateAsync(note);
        }
    }
}
