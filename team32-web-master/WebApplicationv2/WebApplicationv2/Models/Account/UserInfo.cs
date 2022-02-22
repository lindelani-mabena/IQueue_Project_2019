using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplicationv2.Models.Account
{
    public class UserInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public ICollection<IdentityUserClaim> Claims { get; set; }
        public string Email { get; set; }
    }
}
