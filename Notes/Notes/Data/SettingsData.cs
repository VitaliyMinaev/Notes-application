namespace Notes.Data
{
    public class SettingsData
    {
        public int CornerRadius { get; set; }
        public ContainerFonts Fonts { get; set; }
        public bool IsLocked { get; set; }

        public SettingsData()
        {
            CornerRadius = -1;
            Fonts = new ContainerFonts();
            IsLocked = false;
        }
    }
}
