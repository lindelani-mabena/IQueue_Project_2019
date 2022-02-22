using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplicationv2.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
        public TimeSpan CalculateAverageWaitingTime()
        {
            TimeSpan totalTime = TimeSpan.Zero;

            foreach(var ticket in Tickets.Where(x => x.Status.ToLower().Equals("served") && x.InitialDate.Date.Equals(DateTime.Now.Date)))
            {
                totalTime += ticket.Duration;
            }

            int count = Tickets.Count(x => x.InitialDate.Date.Equals(DateTime.Now.Date));

            if (count != 0)
            {
                return totalTime / Tickets.Count(x => x.InitialDate.Date.Equals(DateTime.Now.Date)) * Tickets.Count(x => x.Active && x.InitialDate.Date.Equals(DateTime.Now.Date) && x.Status.ToLower().Equals("pending"));
            }

            return TimeSpan.Zero;
        }
    }
}
