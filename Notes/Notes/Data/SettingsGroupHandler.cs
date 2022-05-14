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
            settings.IsLocked = App.IsLocked;

            return settings;
        }
    }
}
