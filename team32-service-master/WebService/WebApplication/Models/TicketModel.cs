using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebApplication.Models
{
    [DataContract]
    public class TicketModel: Model
    {
        [DataMember]
        public int TicketId { get; set; }
        [Required]
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public UserModel User { get; set; }
        [Required]
        [DataMember]
        public int QueueId { get; set; }
        [DataMember]
        public QueueModel Queue { get; set; }
        [Required]
        [DataMember]
        public int ConsultationId { get; set; }
        [DataMember]
        public ConsultationModel Consultation { get; set; }
        [Required]
        [DataMember]
        public string Token { get; set; }
        [Required]
        [DataMember]
        [DataType(DataType.Time)]
        public TimeSpan AverageWaitingTime { get; set; }
        [Required]
        [DataMember]
        public string Status { get; set; }
        [Required]
        [DataMember]
        public string Type { get; set; }

        public TicketModel(int userId, int queueId, int consultationId,
            string token, string status, string type)
        {
            UserId = userId;
            QueueId = queueId;
            ConsultationId = consultationId;
            Token = token;
            Status = status; // Consider enumeration.
            Type = type; // Consider enumeration.
        }
    }
}