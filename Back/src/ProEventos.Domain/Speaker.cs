using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Domain
{
    public class Speaker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MiniCV { get; set; }
        public string ImageURL { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public IEnumerable<EventSpeaker> EventSpeakers { get; set; }
        public IEnumerable<SocialMedia> SocialMedias { get; set; }
    }
}