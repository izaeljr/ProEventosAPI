using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contexts
{
    public class ProEventosContext : DbContext
    {
        public ProEventosContext(DbContextOptions<ProEventosContext> options) : base(options) 
        {
        }
        public DbSet<EventTest> Events { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<EventSpeaker> EventsSpeakers { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventSpeaker>().
                HasKey(ES => new {ES.EventId, ES.SpeakerId});

            modelBuilder.Entity<EventTest>().HasMany(e => e.SocialMedias).WithOne(sm => sm.Event).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Speaker>().HasMany(s => s.SocialMedias).WithOne(sm => sm.Speaker).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
