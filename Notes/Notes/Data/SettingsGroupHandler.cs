using Xamarin.Forms;

namespace Notes.Data
{
    public class SettingsGroupHandler
    {
        public static SettingsData GroupSettings()
        {
            var settings = new SettingsData();

            settings.CornerRadius = (int)Application.Current.Resources["CornerRadiusFrame"];
            settings.Fonts.TitleFont = Application.Current.Resources["TitleFont"].ToString();
            settings.Fonts.DateFont = Application.Current.Resources["DateFont"].ToString();
            settings.Locked = App.IsLocked;
            settings.Passcode = ((OnPlatform<string>)Application.Current.Resources["PasscodeMD5"]).Default;

            return settings;
        }
    }
}
