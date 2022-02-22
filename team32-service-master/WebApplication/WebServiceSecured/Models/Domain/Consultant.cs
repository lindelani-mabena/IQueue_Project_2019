using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using WebServiceSecured.Models.IdentityModels;

namespace WebServiceSecured.Models.Domain
{
    [DataContract]
    public class Consultant : ApplicationUser
    {
        [DataMember] 
        public Service Service { get; set; }
        [DataMember] 
        public Manager Manager { get; set; }
        [DataMember]
        public bool AssignedAsConsultant { get; set; }
        [DataMember]
        public virtual Branch Branch { get; set; }
        [DataMember]
        public ICollection<Consultation> Consultations { get; set; }
    }
}