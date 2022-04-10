using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Notes.Models
{
    public static class Settings
    {
        private const TheTheme theme = TheTheme.Default;
        public static TheTheme Theme 
        {
            get => (TheTheme)Preferences.Get("Theme", (int)theme);
            set => Preferences.Set("Theme", (int)value); 
        }
    }
}
