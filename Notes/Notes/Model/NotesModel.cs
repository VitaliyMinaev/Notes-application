using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Notes.Model
{
    public class NotesModel : IEnumerable<Note>
    {
        private List<Note> _notes;

        public NotesModel()
        {
            _notes = new List<Note>();
        }
        public void AddNote(Note note)
        {
            _notes.Add(note);
        }

        public void RemoveNote(Note note)
        {
            _notes.Remove(note);
        }
        public void RemoveNote(int index)
        {
            _notes.RemoveAt(index);
        }

        public IEnumerator<Note> GetEnumerator()
        {
            return _notes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
