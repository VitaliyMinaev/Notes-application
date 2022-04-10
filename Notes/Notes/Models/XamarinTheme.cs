using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models
{
    public static class XamarinTheme
    {
        public static void SetTheme()
        {
            switch (Settings.Theme)
            {
                case TheTheme.Default:
                    App.Current.UserAppTheme = Xamarin.Forms.OSAppTheme.Unspecified;
                    break;
                case TheTheme.Light:
                    App.Current.UserAppTheme = Xamarin.Forms.OSAppTheme.Light;
                    break;
                case TheTheme.Dark:
                    App.Current.UserAppTheme = Xamarin.Forms.OSAppTheme.Dark;
                    break;
            }
        }
    }
}
