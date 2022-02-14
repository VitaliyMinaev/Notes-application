using System;
using System.Collections.Generic;
using System.Text;

namespace Notes
{
    public class Note
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }

        public override string ToString()
        {
            return $"{Title}; {Text}; {CreationDate}";
        }
    }
}
