using System.Collections.Generic;
using SQLite;
using Notes.Model;
using System.Threading.Tasks;
using System.Linq;

namespace Notes.Data
{
    public class NotesDB : IDate
    {
        private static NotesDB instance;

        private readonly SQLiteAsyncConnection dataBase;

        NotesDB(string connectionString)
        {
            dataBase = new SQLiteAsyncConnection(connectionString);

            dataBase.CreateTableAsync<Note>().Wait();
        }

        public static NotesDB Initialize(string connectionString)
        {
            if (instance == null)
                instance = new NotesDB(connectionString);

            return instance;
        }

        public void RemoveAsync(Note note)
        {
            dataBase.DeleteAsync<Note>(note.NoteId);
        }

        public void RemoveAsync(int index)
        {
            dataBase.DeleteAsync<Note>(index);
        }

        public Task<List<Note>> GetNotesAsync()
        {
            return dataBase.Table<Note>().ToListAsync();
        }
        public Task<Note> GetNoteAsync(int id)
        {
            return dataBase.Table<Note>().Where(i => i.NoteId == id).FirstOrDefaultAsync();
        }

        public Task<int> AddAsync(Note note)
        {
            return dataBase.InsertAsync(note);
        }
        public Task<int> UpdateAsync(Note note)
        {
            return dataBase.UpdateAsync(note);
        }

        public int GetLastIndexAsync()
        {
            var list = dataBase.Table<Note>().ToListAsync().Result;

            if (list == null || list.Count == 0)
                return -1;

            int maxId = int.MinValue;
            for (int i = 0; i < list.Count; ++i)
                if (list[i].NoteId > maxId)
                    maxId = list[i].NoteId;

            return maxId;
        }
    }
}
