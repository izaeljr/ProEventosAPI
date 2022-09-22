using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence
{
    public interface IProEventosPersistence
    {
        //GENERAL
        void Add<T> (T entity) where T: class;
        void Update<T> (T entity) where T: class;
        void Delete<T> (T entity) where T: class;
        void DeleteRange<T> (T[] entity) where T: class;
        Task<bool> SaveChangesAsync();

        //EVENTS
        Task<EventTest[]> GetAllEventsByThemeAsync(string Theme, bool IncludeSpeakers);
        Task<EventTest[]> GetAllEventsAsync(bool IncludeSpeakers);
        Task<EventTest> GetEventByIdAsync(int EventId, bool IncludeSpeakers);

        //SPEAKERS
        Task<Speaker[]> GetAllSpeakersByNameAsync(string Name, bool IncludeEvents);
        Task<Speaker[]> GetAllSpeakersAsync(bool IncludeEvents);
        Task<Speaker> GetSpeakerByIdAsync(int SpeakerId, bool IncludeEvents);
    }
}