using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServiceSecured.Models.Domain;

namespace WebServiceSecured.Models.Account
{
    public class ProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string PhoneNumber { get; set; }
    }
}