using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AverageWaitingTime { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
