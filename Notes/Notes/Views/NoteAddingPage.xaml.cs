using System;
using Xamarin.Forms;
using Notes.Model;
using Notes.ViewModel;
using Xamarin.CommunityToolkit.Extensions;

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
        private async void SelectColorButton_Clicted(object sender, EventArgs e)
        {
            var result = await Navigation.ShowPopupAsync(new ColorPopup());

            if (result == null)
                return;

            Color color = (Color)result;

            Note colorNote = ((NoteAddingViewModel)BindingContext).AddingNote;

            colorNote.R = color.R;
            colorNote.G = color.G;
            colorNote.B = color.B;

            colorNote.Color = color;

            CollorButton.BackgroundColor = color;
        }
    }
}