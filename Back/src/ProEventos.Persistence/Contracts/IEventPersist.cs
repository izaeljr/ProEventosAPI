using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contracts
{
    public interface IEventPersist
    {
        //EVENTS
        Task<EventTest[]> GetAllEventsByThemeAsync(string Theme, bool IncludeSpeakers = false);
        Task<EventTest[]> GetAllEventsAsync(bool IncludeSpeakers = false);
        Task<EventTest> GetEventByIdAsync(int EventId, bool IncludeSpeakers = false);
    }
}