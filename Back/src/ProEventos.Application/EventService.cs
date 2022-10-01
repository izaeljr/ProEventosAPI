using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Application.Contracts;
using ProEventos.Domain;
using ProEventos.Persistence.Contracts;

namespace ProEventos.Application
{
    public class EventService : IEventService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventPersist _eventPersist;

        public EventService(IGeralPersist geralPersist, IEventPersist eventPersist)
        {
            _geralPersist = geralPersist;
            _eventPersist = eventPersist;
        }
        public async Task<EventTest> AddEvent(EventTest model)
        {
            try
            {
                _geralPersist.Add<EventTest>(model);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _eventPersist.GetEventByIdAsync(model.Id, false);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<EventTest> UpdateEvent(int eventId, EventTest model)
        {
            try
            {
                var evento = await _eventPersist.GetEventByIdAsync(eventId, false);
                if (evento == null) return null;

                model.Id = evento.Id;

                _geralPersist.Update(model);

                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _eventPersist.GetEventByIdAsync(model.Id, false);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvent(int eventId)
        {
            try
            {
                var evento = await _eventPersist.GetEventByIdAsync(eventId, false);
                if (evento == null) throw new Exception("Event for delete not found.");

                _geralPersist.Delete<EventTest>(evento);

                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventTest[]> GetAllEventsAsync(bool IncludeSpeakers = false)
        {
            try
            {
                var events = await _eventPersist.GetAllEventsAsync(IncludeSpeakers);

                if (events == null) return null;

                return events;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventTest[]> GetAllEventsByThemeAsync(string Theme, bool IncludeSpeakers = false)
        {
            try
            {
                var events = await _eventPersist.GetAllEventsByThemeAsync(Theme, IncludeSpeakers);

                if (events == null) return null;

                return events;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventTest> GetEventByIdAsync(int EventId, bool IncludeSpeakers = false)
        {
            try
            {
                var events = await _eventPersist.GetEventByIdAsync(EventId, IncludeSpeakers);

                if (events == null) return null;

                return events;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}