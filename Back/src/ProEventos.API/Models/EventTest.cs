using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.API.Models
{
    public class EventTest
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public string EventDate { get; set; }
        public string Theme { get; set; }
        public int QtdPeople { get; set; }
        public string Batch { get; set; }
        public string ImageURL { get; set; }
    }
}