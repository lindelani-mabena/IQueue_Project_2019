using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WebServiceSecured.Models.Domain;
using WebServiceSecured.Models.IdentityModels;

namespace WebServiceSecured.Models.Account
{
    public class ClientModel: UserModel
    {
        public ICollection<Ticket> Tickets { get; set; }

        public ClientModel() : base() { }

        public ClientModel(string id, string firstName, string lastName,
            string title, string email, string phoneNumber, DateTime
                initialDate, DateTime lastUpdate, DateTime dateAssigned,
            ICollection<Address> addresses, ICollection<Ticket> tickets) : base(id, firstName,
            lastName, title, email, phoneNumber, initialDate, lastUpdate, dateAssigned,
            addresses)
        {
            Tickets = tickets;
        }

        public ClientModel(Client client) : this(client.Id, client.FirstName,
            client.LastName, client.Title, client.Email, client.PhoneNumber,
            client.InitialDate, client.LastUpdate, client.DateAssigned,
            client.Addresses, client.Tickets)
        {

        }

        public override ApplicationUser Convert()
        {
            var client = (Client) base.Convert();
            client.Tickets = Tickets;
            
            return client;
        }
    }
}