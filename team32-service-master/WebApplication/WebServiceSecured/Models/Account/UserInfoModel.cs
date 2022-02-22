using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebServiceSecured.Models.Account
{
    public class UserInfoModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public ICollection<IdentityUserClaim> Claims { get; set; }
        public string Email { get; set; }
        public bool HasRegistered { get; set; }
        public string LoginProvider { get; set; }
    }
}