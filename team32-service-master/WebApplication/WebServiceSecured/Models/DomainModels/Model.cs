using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebServiceSecured.Models.DomainModels
{
    [DataContract]
    public abstract class Model
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Email  { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public virtual ICollection<IdentityUserClaim> Claims { get; set; }
    }
}