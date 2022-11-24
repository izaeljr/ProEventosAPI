using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;
using ProEventos.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using ProEventos.Persistence.Contexts;
using ProEventos.Domain.Identity;

namespace ProEventos.Persistence
{
    public class SpeakerPersist : ISpeakerPersist
    {
        private readonly ProEventosContext _context;

        public SpeakerPersist(ProEventosContext context)
        {
            _context = context;
        }

        public async Task<Speaker[]> GetAllSpeakersAsync(bool includeEvents = false)
        {
            IQueryable<Speaker> query = _context.Speakers.Include(s => s.SocialMedias);

            if (includeEvents)
            {
                query = query.Include(s => s.EventSpeakers).ThenInclude(se => se.Event);
            }

            query = query.AsNoTracking().OrderBy(s => s.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Speaker[]> GetAllSpeakersByNameAsync(string name, bool includeEvents)
        {
            IQueryable<Speaker> query = _context.Speakers.Include(s => s.SocialMedias);

            if (includeEvents)
            {
                query = query.Include(s => s.EventSpeakers).ThenInclude(se => se.Event);
            }

            query = query.AsNoTracking().OrderBy(s => s.Id).Where(s => s.User.FirstName.ToLower().Contains(name.ToLower()) && s.User.LastName.ToLower().Contains(name.ToLower()));

            return await query.ToArrayAsync();
        }
        public async Task<Speaker> GetSpeakerByIdAsync(int speakerId, bool includeEvents)
        {
            IQueryable<Speaker> query = _context.Speakers.Include(s => s.SocialMedias);

            if (includeEvents)
            {
                query = query.Include(s => s.EventSpeakers).ThenInclude(se => se.Event);
            }

            query = query.AsNoTracking().OrderBy(s => s.Id).Where(s => s.Id == speakerId);

            return await query.FirstOrDefaultAsync();
        }
    }
}