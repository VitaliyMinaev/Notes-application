using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Notes.ViewModel;
using Xamarin.CommunityToolkit.Extensions;
using Notes;
using System.Threading.Tasks;
using Notes.Data;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPage : ContentPage
    {
        private static int _onAppearingCounter;
        public NotesPage()
        {
            InitializeComponent();
            CheckIsLockedAsync();
        }

        private async Task CheckIsLockedAsync()
        {
            if (App.IsLocked == true)
                await Navigation.PushAsync(new LockPage());
        }

        protected override async void OnAppearing()
        {
            // Circle color messaging center
            MessagingCenter.Send(this, nameof(NotesPage));
            ShowOrHideControlsAsync();

            _onAppearingCounter += 1;
        }

        private async void ShowOrHideControlsAsync()
        {
            await Task.Run(() =>
            {
                var notes = ((NotesListViewModel)BindingContext).Notes;

                if (notes.Count == 0)
                    HideListViewShowLabel();
                else
                    HideLabelShowListView();
            });

        }

        private void HideLabelShowListView()
        {
            noteList.IsVisible = true;

            FrameLabel.IsVisible = false;
            LabelNoElements.IsVisible = false;
        }
        private void HideListViewShowLabel()
        {
            noteList.IsVisible = false;

            FrameLabel.IsVisible = true;
            LabelNoElements.IsVisible = true;
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

            if(notes.Count == 0)
                HideListViewShowLabel();
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

        private async void ToolBarItem_Lock_Clicked(object sender, EventArgs e)
        {
            App.IsLocked = true;
            await Navigation.PushAsync(new LockPage());

            await Task.Run(() =>
            {
                SettingsData settings = SettingsGroupHandler.GroupSettings(); 
                var fileHandler = SettingsFileHandler.GetInstance();
                fileHandler.RewriteSettingsFile(settings);
            });
        }
    }
}