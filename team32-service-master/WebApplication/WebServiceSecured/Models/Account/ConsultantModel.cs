using System;
using System.Collections.Generic;
using WebServiceSecured.Models.Domain;
using WebServiceSecured.Models.IdentityModels;

namespace WebServiceSecured.Models.Account
{
    public class ConsultantModel: UserModel
    {
        public Branch Branch { get; set; }
        public ICollection<Consultation> Consultations { get; set; }

        public ConsultantModel() : base() { }

        public ConsultantModel(string id, string firstName, string lastName,
            string title, string email, string phoneNumber, DateTime
                initialDate, DateTime lastUpdate, DateTime dateAssigned,
            ICollection<Address> addresses, 
            ICollection<Consultation> consultations, Branch branch) : base(id, firstName,
            lastName, title, email, phoneNumber, initialDate, lastUpdate, dateAssigned,
            addresses)
        {
            Branch = branch;
            Consultations = consultations;
        }

        public ConsultantModel(Consultant consultant) : this(consultant.Id, consultant.FirstName,
            consultant.LastName, consultant.Title, consultant.Email, consultant.PhoneNumber,
            consultant.InitialDate, consultant.LastUpdate, consultant.DateAssigned,
            consultant.Addresses,  consultant.Consultations, consultant.Branch)
        {
        }

        public override ApplicationUser Convert()
        {
            var consultant = (Consultant) base.Convert();

            consultant.Branch = Branch;
            consultant.Consultations = Consultations;

            return consultant;
        }
    }
}