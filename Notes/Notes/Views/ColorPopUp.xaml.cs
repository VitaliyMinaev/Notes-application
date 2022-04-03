using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.PlatformConfiguration.iOSSpecific;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorPopup : Popup
    {
        public string Text { get; set; }
        public ColorPopup()
        {
            InitializeComponent();
        }

        private void ButtonColor_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            Color color = button.BackgroundColor;

            Dismiss(color);
        }
    }
}