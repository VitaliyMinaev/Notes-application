using Notes.Models;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Data
{
    public class SettingsFileHandler
    {
        private string _fileName;
        private static SettingsFileHandler _instance;

        SettingsFileHandler(string fileName)
        {
            _fileName = fileName;
        }
        public static SettingsFileHandler GetInstance(string fileName = "Settings.txt")
        {
            if (_instance == null)
                _instance = new SettingsFileHandler(fileName);

            return _instance;
        }

        public void RewriteSettingsFile(SettingsData settings)
        {
            string dataInFile = $"CornerRadius:{settings.CornerRadius}\n" +
                                        $"TitleFont:{settings.Fonts.TitleFont}\n" +
                                        $"DateFont:{settings.Fonts.DateFont}\n" +
                                        $"IsLocked:{settings.Locked}\n" +
                                        $"Passcode:{settings.Passcode}\n" +
                                        $"Question:{settings.QuestionAndAnswer.QuestionText}\n" +
                                        $"Answer:{settings.QuestionAndAnswer.AnswerText}";

            File.WriteAllText(Path.Combine(App.FolderPath, _fileName), dataInFile);
        }
        public void ClearSettingsFile()
        {
            string dataInFile = $"CornerRadius:30\n" +
                                        $"TitleFont:Default\n" +
                                        $"DateFont:Default\n" +
                                        $"IsLocked:{LockEntity.Undefined}\n" +
                                        $"Passcode:None\n" +
                                        $"Question:None\n" +
                                        $"Answer:None";

            File.WriteAllText(Path.Combine(App.FolderPath, _fileName), dataInFile);
        }
    }
}
