using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Notes.ViewModel;
using Xamarin.CommunityToolkit.Extensions;
using Notes.Model;
using System.Collections.Generic;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPage : ContentPage
    {
        public NotesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            MessagingCenter.Send(this, nameof(NotesPage));
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

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var result = await Navigation.ShowPopupAsync(new ColorPopup());

            if (result == null)
                return;

            Color color = (Color)result;

            var tappedEventsArgs = (TappedEventArgs)e;

            var notes = ((NotesListViewModel)BindingContext).Notes;

            var colorNote = notes.Where(note => note.NoteId == (int)tappedEventsArgs.Parameter)
                .FirstOrDefault();

            colorNote.R = color.R;
            colorNote.G = color.G;
            colorNote.B = color.B;

            colorNote.Color = color;

            MessagingCenter.Send(this, nameof(NotesListViewModel), colorNote);
        }
    }
}