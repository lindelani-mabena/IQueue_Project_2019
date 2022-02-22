using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApplication.Models
{
    [DataContract]
    public class ConsultationModel: Model
    {
        [DataMember]
        public int ConsultationId { get; set; }
        [DataMember]
        [Required]        
        public int ServiceId { get; set; }
        [Required]
        [DataMember]
        public string Teller { get; set; }
        [Required]
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        [DataType(DataType.Time)]
        public TimeSpan StartAt { get; set; }
        [DataMember]
        [DataType(DataType.Time)]
        public TimeSpan EndAt { get; set; }
        [DataMember]
        [DataType(DataType.Time)]
        public TimeSpan Duration { get; set; }
        [DataMember] public List<TicketModel> Tickets { get; set; }

        public ConsultationModel(int serviceId, string teller, string status)
        {
            ServiceId = serviceId;
            Teller = teller;
            Status = status;
            Tickets = new List<TicketModel>();
        }
    }
}