using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Notes.ViewModel;

namespace Notes
{
    public partial class MainPage : ContentPage
    {
        public IList<Note> Notes { get; set; }
        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            Notes = new List<Note>
            {
                new Note { Title = "Item 1", Text = "Item 1 text", CreationDate = DateTime.Now},
                new Note { Title = "Item 2", Text = "Item 2 text", CreationDate = DateTime.Now},
                new Note { Title = "Item 3", Text = "Item 3 text", CreationDate = DateTime.Now}
            };

            BindingContext = this;
        }

        private async void ButtonClicked(object sender, EventArgs e)
        {
            
        }

        private void noteList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}
