using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Notes.Model;
using Notes.ViewModel;
using System.Collections.ObjectModel;

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
            base.OnAppearing();
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

        private async void TapGestureRecognizer_Tapped_Remove(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Delete note", "Are you sure?", "Yes", "No");

            if (result == false)
                return;

            var tappedEventsArgs = (TappedEventArgs)e;

            var notes = ((NotesListViewModel)BindingContext).Notes;

            var noteToRemove = notes.Where(note => note.NoteId == (int)tappedEventsArgs.Parameter)
                .FirstOrDefault();

            notes.Remove(noteToRemove);

            App.NotesDataBase.RemoveAsync(noteToRemove);
        }
    }
}