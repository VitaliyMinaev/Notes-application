using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Data
{
    public class ContainerFonts
    {
        public string TitleFont { get; set; }
        public string DateFont { get; set; }
        
        public ContainerFonts()
        {
            TitleFont = null;
            DateFont = null;
        }
    }
}
