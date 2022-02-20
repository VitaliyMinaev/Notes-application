using System;
using Xamarin.Forms;
using Notes.Model;
using Notes.ViewModel;

namespace Notes.Views
{
    public partial class NoteAddingPage : ContentPage
    {
        public NoteAddingPage(Note note = null)
        {
            InitializeComponent();

            if (note != null)
                ((NoteAddingViewModel)BindingContext).AddingNote = note;
        }
        private async void ButtonSave_Clicked(object sender, EventArgs e)
        {
            Note note = ((NoteAddingViewModel)BindingContext).AddingNote;

            MessagingCenter.Send(this, nameof(NoteAddingPage), note);

            await Navigation.PopAsync();
        }
    }
}