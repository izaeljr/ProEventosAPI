using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Application.Contracts
{
    public interface IEventService
    {
        Task<EventTest> AddEvent(EventTest model);
        Task<EventTest> UpdateEvent(int eventId, EventTest model);
        Task<bool> DeleteEvent(int eventId);

        Task<EventTest[]> GetAllEventsByThemeAsync(string Theme, bool IncludeSpeakers = false);
        Task<EventTest[]> GetAllEventsAsync(bool IncludeSpeakers = false);
        Task<EventTest> GetEventByIdAsync(int EventId, bool IncludeSpeakers = false);
    }
}