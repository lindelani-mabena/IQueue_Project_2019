using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;
using WebServiceSecured.Models.IdentityModels;

namespace WebServiceSecured.Models.Domain
{
    [DataContract]
    public class Manager: ApplicationUser
    {
        [DataMember]
        public bool AssignedAsManager { get; set; }
        [DataMember]
        public virtual List<Consultant> Consultants { get; set; }
        [DataMember]
        public virtual List<Branch> Branches { get; set; }
    }
}