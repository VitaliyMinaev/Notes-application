using Notes.Model;
using Notes.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Notes.ViewModel
{
    public class BasketNoteViewModel
    {
        public ObservableCollection<Note> BasketNotes { get; set; }

        public BasketNoteViewModel()
        {
            List<Note> notes = App.BasketDataBase.GetNotesAsync().Result;

            BasketNotes = new ObservableCollection<Note>(notes);

            SubscribeToMessageCenter();
        }

        public void SubscribeToMessageCenter()
        {
            MessagingCenter.Subscribe<BasketPage>(this, nameof(BasketNoteViewModel), (page) =>
            {
                foreach (var colorNote in BasketNotes)
                    colorNote.Color = Color.FromRgb(colorNote.R, colorNote.G, colorNote.B);
            });

            MessagingCenter.Subscribe<NotesPage, Note>(this, nameof(NotesPage), (page, note) =>
            {
                AddNoteAsync(note);
            });
        }

        private async void AddNoteAsync(Note note)
        {
            await Task.Run(() =>
            {
                note.NoteId = App.BasketDataBase.GetLastIndexAsync() + 1;

                BasketNotes.Add(note);
                App.BasketDataBase.AddAsync(note);
            });
        }
    }
}
