using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace WebServiceSecured.Models.Domain
{
    [DataContract]
    public class Service
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public DateTime InitialDate { get; set; }
        [DataMember]
        public DateTime LastUpdate { get; set; }
        [DataMember]
        public virtual ICollection<Ticket> Tickets { get; set; }
        public ICollection<Branch> Branches { get; set; }

        public Service()
        {
            Name = "";
            Description = "";
            InitialDate = DateTime.Now;
            LastUpdate = DateTime.Now;
        }

        public TimeSpan CalculateAverageWaitingTime()
        {
            var totalTime = TimeSpan.Zero;

            foreach (var ticket in Tickets.Where(x => x.Status.ToLower().Equals("served") && x.InitialDate.Date.Equals(DateTime.Now.Date)))
            {
                totalTime += ticket.Duration;
            }

            var pendingTime = TimeSpan.Zero;

            foreach (var ticket in Tickets.Where(x => x.Status.ToLower().Equals("pending") && x.InitialDate.Date.Equals(DateTime.Now.Date)))
            {
                pendingTime += TimeSpan.FromTicks(DateTime.Now.Ticks).Subtract(TimeSpan.FromTicks(ticket.InitialDate.Ticks));
            }
            
            var count = Tickets.Count(x => x.InitialDate.Date.Equals(DateTime.Now.Date));

            if (count != 0)
            {
                var total = totalTime.Ticks;

                var aver = total / Tickets.Count(x => x.InitialDate.Date.Equals(DateTime.Now.Date)) * Tickets.Count(x => x.Active && x.InitialDate.Date.Equals(DateTime.Now.Date) && x.Status.ToLower().Equals("pending"));

                var returnSpan = new TimeSpan(aver);

                return returnSpan;
            }

            return TimeSpan.Zero;
        }
    }
}