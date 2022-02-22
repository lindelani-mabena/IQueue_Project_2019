using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Security.Claims;
using WebServiceSecured.Models.IdentityModels;

namespace WebServiceSecured.Models.Domain
{
    [DataContract]
    public class Client: ApplicationUser
    {
        [DataMember]
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}