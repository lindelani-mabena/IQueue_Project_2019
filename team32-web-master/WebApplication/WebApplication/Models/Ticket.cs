using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public bool Active { get; set; }
        public string Status { get; set; } // Queued, Consultantion, Inactive
        public DateTime InitialTime { get; set; }
        public TimeSpan ElapsedTime { get; set; }
        public TimeSpan WaitingTime { get; set; }
    }
}
