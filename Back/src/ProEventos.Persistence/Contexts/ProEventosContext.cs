﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Domain.Identity;

namespace ProEventos.Persistence.Contexts
{
    public class ProEventosContext : IdentityDbContext<User, Role, int, 
                                                        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                                        IdentityRoleClaim<int>, IdentityUserToken<int>>
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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole => 
                {
                    userRole.HasKey(ur => new { ur.UserId, ur.RoleId});

                    userRole.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId).IsRequired();

                    userRole.HasOne(ur => ur.User).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.UserId).IsRequired();
                }
            );

            modelBuilder.Entity<EventSpeaker>().
                HasKey(ES => new {ES.EventId, ES.SpeakerId});

            modelBuilder.Entity<EventTest>().HasMany(e => e.SocialMedias).WithOne(sm => sm.Event).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Speaker>().HasMany(s => s.SocialMedias).WithOne(sm => sm.Speaker).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
