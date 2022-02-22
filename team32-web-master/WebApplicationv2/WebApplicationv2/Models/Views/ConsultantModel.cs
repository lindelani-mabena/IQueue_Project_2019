using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationv2.Models.Views
{
    public class ConsultantModel: UserModel
    {
        public Branch Branch { get; set; }
        public ICollection<Consultation> Consultations { get; set; }
        public ConsultantModel() : base() { }
    }
}
