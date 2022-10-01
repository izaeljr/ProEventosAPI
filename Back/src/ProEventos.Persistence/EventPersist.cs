using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;
using ProEventos.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using ProEventos.Persistence.Contexts;

namespace ProEventos.Persistence
{
    public class EventPersist : IEventPersist
    {
        private readonly ProEventosContext _context;

        public EventPersist(ProEventosContext context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<EventTest[]> GetAllEventsAsync(bool includeSpeakers = false)
        {
            IQueryable<EventTest> query = _context.Events.Include(e => e.Lotes).Include(e => e.SocialMedias);

            if (includeSpeakers)
            {
                query = query.Include(e => e.EventsSpeakers).ThenInclude(se => se.Speaker);
            }

            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<EventTest[]> GetAllEventsByThemeAsync(string theme, bool includeSpeakers)
        {
            IQueryable<EventTest> query = _context.Events.Include(e => e.Lotes).Include(e => e.SocialMedias);

            if (includeSpeakers)
            {
                query = query.Include(e => e.EventsSpeakers).ThenInclude(pe => pe.Speaker);
            }

            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Theme.ToLower().Contains(theme.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<EventTest> GetEventByIdAsync(int eventId, bool includeSpeakers)
        {
            IQueryable<EventTest> query = _context.Events.Include(e => e.Lotes).Include(e => e.SocialMedias);

            if (includeSpeakers)
            {
                query = query.Include(e => e.EventsSpeakers).ThenInclude(se => se.Speaker);
            }

            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Id == eventId);

            return await query.FirstOrDefaultAsync();
        }
    }
}