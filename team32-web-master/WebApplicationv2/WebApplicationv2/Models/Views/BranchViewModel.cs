using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationv2.Models.Views
{
    public class BranchViewModel
    {
        public Branch Branch { get; set; }
        public List<Branch> Branches { get; set; }
    }
}
