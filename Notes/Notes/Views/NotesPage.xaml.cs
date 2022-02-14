using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Notes.Model;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPage : ContentPage
    {
        public NotesModel Notes { get; set; }
        public NotesPage()
        {
            InitializeComponent();

            Notes = new NotesModel();

            Notes.AddNote(new Note { Title = "Item 1", Text = "Item 1 text", CreationDate = DateTime.Now });
            Notes.AddNote(new Note { Title = "Item 2", Text = "Item 2 text", CreationDate = DateTime.Now });
            Notes.AddNote(new Note { Title = "Item 3", Text = "Item 3 text", CreationDate = DateTime.Now });
        }
        protected override void OnAppearing()
        {
            noteList.ItemsSource = Notes;
        }

        private async void ButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NoteAddingPage));
        }

        private void noteList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
        }
    }
}