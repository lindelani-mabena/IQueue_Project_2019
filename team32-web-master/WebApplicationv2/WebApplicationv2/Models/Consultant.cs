using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebApplicationv2.Models
{
    public class Consultant: UserModel
    {
        public int ServiceId { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public Service Service { get; set; }
        public ICollection<Consultation> Consultations { get; set; }
    }
}
