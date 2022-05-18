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
            settings.QuestionAndAnswer.QuestionText = ((OnPlatform<string>)Application.Current.Resources["Question"]).Default;
            settings.QuestionAndAnswer.AnswerText = ((OnPlatform<string>)Application.Current.Resources["Answer"]).Default;

            return settings;
        }
        public static SettingsData GroupSettings(QuestionEntity question)
        {
            var settings = new SettingsData();

            settings.CornerRadius = (int)Application.Current.Resources["CornerRadiusFrame"];
            settings.Fonts.TitleFont = Application.Current.Resources["TitleFont"].ToString();
            settings.Fonts.DateFont = Application.Current.Resources["DateFont"].ToString();
            settings.Locked = App.IsLocked;
            settings.Passcode = ((OnPlatform<string>)Application.Current.Resources["PasscodeMD5"]).Default;
            settings.QuestionAndAnswer.QuestionText = question.QuestionText;
            settings.QuestionAndAnswer.AnswerText = question.AnswerText;

            return settings;
        }
    }
}
