using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServiceSecured.Models.Domain;
using WebServiceSecured.Models.IdentityModels;

namespace WebServiceSecured.Models.Account
{
    public class AdminModel: UserModel
    {
        public AdminModel() { }

        public AdminModel(Admin admin) : base(admin.Id, admin.FirstName,
            admin.LastName, admin.Title, admin.Email, admin.PhoneNumber,
            admin.InitialDate, admin.LastUpdate, admin.DateAssigned,
            admin.Addresses)
        {

        }
    }
}