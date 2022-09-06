using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Domain
{
    public class SocialMedia
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int? EventId { get; set; }
        public EventTest Event { get; set; }
        public int? SpeakerId { get; set; }
        public Speaker Speaker { get; set; }
    }
}