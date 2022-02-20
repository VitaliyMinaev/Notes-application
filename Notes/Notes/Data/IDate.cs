using Notes.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Data
{
    public interface IDate
    {
        Task<int> AddAsync(Note note);
        Task<int> UpdateAsync(Note note);
        Task<List<Note>> GetNotesAsync();
        Task<Note> GetNoteAsync(int id);

        void RemoveAsync(Note note);
        void RemoveAsync(int index);
        void RemoveAllAsync();

        int GetLastIndexAsync();
    }
}
