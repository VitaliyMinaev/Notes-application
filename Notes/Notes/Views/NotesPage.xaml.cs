using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Notes.ViewModel;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPage : ContentPage
    {
        public NotesPage()
        {
            InitializeComponent();
        }

        private async void ButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NoteAddingPage());
        }

        private async void TapGestureRecognizer_Tapped_Edit(object sender, EventArgs e)
        {
            var tappedEventsArgs = (TappedEventArgs)e;

            var notes = ((NotesListViewModel)BindingContext).Notes;
            var noteChange = notes.Where(note => note.NoteId == (int)tappedEventsArgs.Parameter)
                .FirstOrDefault();

            await Navigation.PushAsync(new NoteAddingPage(noteChange));
        }

        private void TapGestureRecognizer_Tapped_Remove(object sender, EventArgs e)
        {
            var tappedEventsArgs = (TappedEventArgs)e;

            var notes = ((NotesListViewModel)BindingContext).Notes;

            var noteToRemove = notes.Where(note => note.NoteId == (int)tappedEventsArgs.Parameter)
                .FirstOrDefault();

            notes.Remove(noteToRemove);

            App.NotesDataBase.RemoveAsync(noteToRemove);

            MessagingCenter.Send(this, nameof(NotesPage), noteToRemove);
        }
    }
}