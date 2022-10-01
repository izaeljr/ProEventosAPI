using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Domain
{
    public class Lote
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime? InitialDate { get; set; }
        public DateTime? EndDate { get; set; }
       // public int Quantity { get; set; }
        public int EventId { get; set; }
        public EventTest Event { get; set; }
    }
}