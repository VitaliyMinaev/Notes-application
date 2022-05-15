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
            string dataInFile = $"CornerRadius:{settings.CornerRadius} " +
                                        $"TitleFont:{settings.Fonts.TitleFont} " +
                                        $"DateFont:{settings.Fonts.DateFont} " +
                                        $"IsLocked:{settings.Locked}";

            File.WriteAllText(Path.Combine(App.FolderPath, _fileName), dataInFile);
        }
        public void ClearSettingsFile()
        {
            string dataInFile = $"CornerRadius:30 " +
                                        $"TitleFont:Default " +
                                        $"DateFont:Default " +
                                        $"IsLocked:{LockEntity.Undefined}";

            File.WriteAllText(Path.Combine(App.FolderPath, _fileName), dataInFile);
        }
    }
}
