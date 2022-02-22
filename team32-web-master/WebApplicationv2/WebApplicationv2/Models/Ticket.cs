using System;

namespace WebApplicationv2.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public Client Client { get; set; }
        public string ConsultantId { get; set; }
        public Consultant Consultant { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public string Token { get; set; }
        public string Status { get; set; }
        public bool Active { get; set; }
        public string Type { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public TimeSpan EstimatedWaitingTime { get; set; }
        public TimeSpan StartAt { get; set; }
        public TimeSpan EndAt { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
