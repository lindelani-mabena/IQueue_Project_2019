using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServiceSecured.Models.Domain;
using WebServiceSecured.Models.IdentityModels;

namespace WebServiceSecured.Models.Account
{
    public class ManagerModel: UserModel
    {
        public int BranchId { get; set; }

        public ManagerModel(): base() { }

        public ManagerModel(string id, string firstName, string lastName,
            string title, string email, string phoneNumber, DateTime
                initialDate, DateTime lastUpdate, DateTime dateAssigned,
            ICollection<Address> addresses) : base(id, firstName,
            lastName, title, email, phoneNumber, initialDate, lastUpdate, dateAssigned,
            addresses)
        {

        }

        public ManagerModel(Manager manager) : this(manager.Id, manager.FirstName,
            manager.LastName, manager.Title, manager.Email, manager.PhoneNumber,
            manager.InitialDate, manager.LastUpdate, manager.DateAssigned,
            manager.Addresses)
        {

        }

        public override ApplicationUser Convert()
        {
            var manager = (Manager) base.Convert();

            return manager;
        }

    }
}