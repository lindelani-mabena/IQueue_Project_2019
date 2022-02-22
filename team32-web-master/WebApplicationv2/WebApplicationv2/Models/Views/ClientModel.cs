using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationv2.Models.Views
{
    public class ClientModel: UserModel
    {
        public ICollection<Ticket> Tickets { get; set; }
    }
}
