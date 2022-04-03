using SQLite;
using System;
using Xamarin.Forms;

namespace Notes.Model
{
    public class Note
    {
        [PrimaryKey]
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public double R { get; set; }
        public double G { get; set; }
        public double B { get; set; }

        [Ignore]
        public Color Color { get; set; }

        public Note()
        {
            NoteId = -1;

            Title = null;
            Text = null;
            CreationDate = DateTime.Now;
            R = G = B = 0;
        }
        public Note(string title, string text, DateTime date)
        {
            NoteId = -1;
            
            Title = title;
            Text = text;
            CreationDate = date;
            R = G = B = 0;
        }
    }
}
