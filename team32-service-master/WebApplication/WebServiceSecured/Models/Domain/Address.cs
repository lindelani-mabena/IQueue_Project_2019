using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.Runtime.Serialization;

namespace WebServiceSecured.Models.Domain
{
    [DataContract]
    public class Address
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Address1 { get; set; } 
        [DataMember]
        public string Address2 { get; set; } 
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string Province { get; set; } 
        [DataMember]
        public string Zip { get; set; }

        [DataMember] public string Longitude { get; set; }
        [DataMember] public string Latitude { get; set; }

        [DataMember]
        public int BranchId { get; set; }
        [DataMember]
        public DateTime InitialDate { get; set; }
        [DataMember]
        public DateTime LastUpdate { get; set; }
    }
}