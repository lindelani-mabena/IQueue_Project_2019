using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebServiceSecured.Models.Domain
{
    [DataContract]
    public class Ticket
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        public Client Client { get; set; }
        public Consultant Consultant { get; set; }
        public Branch Branch{ get; set; }
        public Service Service { get; set; }
        [DataMember]
        public string Token { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public bool Active { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public DateTime InitialDate { get; set; }
        [DataMember]
        public DateTime LastUpdate { get; set; }
        [DataMember]
        public TimeSpan EstimatedWaitingTime { get; set; }
        [DataMember]
        public TimeSpan StartAt { get; set; }
        [DataMember]
        public TimeSpan EndAt { get; set; }
        [DataMember]
        public TimeSpan Duration { get; set; }

        public Ticket()
        {
            Duration = TimeSpan.Zero;
            InitialDate = DateTime.Now;
            LastUpdate = DateTime.Now;
            StartAt = TimeSpan.Zero;
            EndAt = TimeSpan.Zero;
            Duration = TimeSpan.Zero;
            EstimatedWaitingTime = TimeSpan.Zero;
            Status = "Pending";
            Active = true;
            Type = "";
        }

        public void Join(int ticketNumber, Branch branch, Service service, string type)
        {
            Branch = branch;
            Token = service.Name.Substring(0, 3).ToUpper() + " Ticket " + Check(ticketNumber);
            Service = service;
            Type = type;
            LastUpdate = DateTime.Now;
        }

        public void Consult(Consultant consultant, string action, bool start)
        {
            if (start)
            {
                Consultant = consultant;
                LastUpdate = DateTime.Now;
                Status = action;
                StartAt = DateTime.Now.TimeOfDay;
                Duration = TimeSpan.Zero;
            }
            else
            {
                Active = false;
                Status = action;
                LastUpdate = DateTime.Now;
                EndAt = DateTime.Now.TimeOfDay;

                var duration = TimeSpan.Zero;

                if (EndAt > StartAt)
                {
                    duration = EndAt.Subtract(StartAt);
                }
                else
                {
                    duration = StartAt.Subtract(EndAt);
                }
                Duration = duration;
            }
            
        }

        private static string Check(int x)
        {
            return x < 10 ? "0" + x : x.ToString();
        }
    }
}