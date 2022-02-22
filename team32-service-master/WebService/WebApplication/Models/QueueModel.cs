using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Antlr.Runtime.Misc;

namespace WebApplication.Models
{
    [DataContract]
    public class QueueModel : Model
    {
        [DataMember] public int QueueId { get; set; }
        [DataMember] [Required] public int ServiceId { get; set; }
        [DataMember] public ServiceModel Service { get; set; }
        [DataMember] [Required]public string Status { get; set; }
        [DataMember] public List<TicketModel> Tickets { get; set; }
        [DataType(DataType.Time)] [DataMember] public TimeSpan AverageWaitingTime { get; set; }

        public QueueModel(int serviceId, string status)
        {
            ServiceId = serviceId;
            Status = status;
            Tickets = new ListStack<TicketModel>();
        }
    }
}