using System.Collections.Generic;

namespace WebApplicationv2.Models
{
    public class Client: UserModel
    {
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
