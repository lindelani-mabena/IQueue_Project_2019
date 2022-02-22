using System.Collections.Generic;
using System.Runtime.Serialization;
using WebServiceSecured.Models.Domain;

namespace WebServiceSecured.Models.DomainModels
{
    [DataContract]
    public class ClientModel: Model
    {
        [DataMember]
        public ICollection<Address> Addresses { get; set; }
        [DataMember] public ICollection<Ticket> Tickets { get; set; }
    }
}