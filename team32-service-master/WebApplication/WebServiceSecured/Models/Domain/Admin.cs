using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using WebServiceSecured.Models.IdentityModels;

namespace WebServiceSecured.Models.Domain
{
    [DataContract]
    public class Admin: ApplicationUser
    {
        [DataMember]
        public virtual List<Branch> Branches { get; set; }
    }
}