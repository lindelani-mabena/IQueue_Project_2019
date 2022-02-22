using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Client: UserModel
    {
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
