using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    interface IBranchRepository
    {
        List<BranchModel> Branches();
        BranchModel GetBranch(int branchId);
        BranchModel UpdateBranch(int branchId, BranchModel branchModel);
        BranchModel DeleteBranch(int branchId);
        BranchModel AddBranch(BranchModel branchModel);
    }
}
