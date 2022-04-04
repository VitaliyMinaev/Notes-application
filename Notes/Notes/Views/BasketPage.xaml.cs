using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Notes.ViewModel;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasketPage : ContentPage
    {
        public BasketPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            MessagingCenter.Send(this, nameof(BasketNoteViewModel));
            ShowOrHideControls();
        }

        private void ShowOrHideControls()
        {
            var notes = ((BasketNoteViewModel)BindingContext).BasketNotes;

            if (notes.Count == 0)
                HideListViewShowLabel();
            else
                HideLabelShowListView();

        }

        private void HideLabelShowListView()
        {
            basketList.IsVisible = true;
            LabelNoElements.IsVisible = false;
        }

        private void HideListViewShowLabel()
        {
            basketList.IsVisible = false;
            LabelNoElements.IsVisible = true;
        }

        private void Tapped_Upload(object sender, EventArgs e)
        {
            var tappedEventsArgs = (TappedEventArgs)e;

            var notes = ((BasketNoteViewModel)BindingContext).BasketNotes;
            var noteToRemove = notes.Where(note => note.NoteId == (int)tappedEventsArgs.Parameter)
                .FirstOrDefault();

            ((BasketNoteViewModel)BindingContext).BasketNotes.Remove(noteToRemove);

            App.BasketDataBase.RemoveAsync(noteToRemove);

            MessagingCenter.Send(this, nameof(BasketPage), noteToRemove);

            if(notes.Count == 0)
                HideListViewShowLabel();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Delete notes", "Are you sure?", "Yes", "No");

            if (result == false)
                return;

            ((BasketNoteViewModel)BindingContext).BasketNotes.Clear();
            App.BasketDataBase.RemoveAllAsync();

            HideListViewShowLabel();
        }
    }
}