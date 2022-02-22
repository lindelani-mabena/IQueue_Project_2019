using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using WebServiceSecured.Models.IdentityModels;

namespace WebServiceSecured.Models.Domain
{
    [DataContract]
    public class Consultation
    {
        [Key]
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public Consultant Consultant { get; set; }
        [DataMember]
        public Client Client { get; set; }
        [DataMember]
        public string Comments { get; set; }
        [DataMember]
        public string FeedBack { get; set; }
        [DataMember]
        public DateTime InitialDate { get; set; }
        [DataMember]
        public DateTime LastUpdate { get; set; }
        [DataMember]
        public int Rating { get; set; }
        [DataMember]
        public Ticket Ticket { get; set; }

        public Consultation()
        {

        }

        public Consultation(string comments)
        {
            FeedBack = "";
            InitialDate = DateTime.Now;
            Rating = 0;
            Comments = comments;
            LastUpdate = DateTime.Now;
        }
    }
}