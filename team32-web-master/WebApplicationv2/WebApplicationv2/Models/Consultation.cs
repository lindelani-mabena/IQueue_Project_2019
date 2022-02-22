using System;

namespace WebApplicationv2.Models
{
    public class Consultation
    {
        public string Id { get; set; }
        public string ConsultantId { get; set; }
        public string ClientId { get; set; }
        public string FeedBack { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public int Rating { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
