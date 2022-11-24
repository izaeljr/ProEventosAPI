using System;
using System.Collections.Generic;
using ProEventos.Domain.Identity;

namespace ProEventos.Domain
{
    public class Speaker
    {
        public int Id { get; set; }
        public string MiniCV { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<EventSpeaker> EventSpeakers { get; set; }
        public IEnumerable<SocialMedia> SocialMedias { get; set; }
    }
}