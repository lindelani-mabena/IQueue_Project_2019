using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using WebApplication.Models.Abstraction;

namespace WebApplication.Models
{
    public class BranchRepository: IBranchRepository
    {
        private readonly IqueueDbDataContext _db;
        private readonly ModelFactory _modelFactory;
        public BranchRepository(IqueueDbDataContext db)
        {
            _db = db;
            _modelFactory = new ModelFactory(db);
        }

        public List<BranchModel> Branches()
        {
            var branchModels = new List<BranchModel>();
            var branches = _db.Branches;

            foreach (var branch in branches)
            {
                branchModels.Add((BranchModel) 
                    _modelFactory.CreateBranchModel(branch));
            }
            return branchModels;
        }
        public BranchModel GetBranch(int branchId)
        {
            var branch = _db.Branches.FirstOrDefault(
                x => x.Branch_Id == branchId);
            var branchModel = _modelFactory.CreateBranchModel(branch);
            return (BranchModel) branchModel;
        }
        public BranchModel UpdateBranch(int branchId, BranchModel branchModel)
        {
            var branch = _db.Branches.FirstOrDefault(x => x.Branch_Id == branchId);
            if (branch == null) return null;

            branch.Name = branchModel.Name;
            branch.Code = branchModel.Code;
            branch.Phone = branchModel.Phone;
            branch.Email = branchModel.Email;
            branch.Address_Id = branchModel.AddressId;
            branch.Last_Update = branchModel.GetLastUpdateDate();

            _db.SubmitChanges();

            return (BranchModel) _modelFactory.CreateBranchModel(branch);
        }
        public BranchModel DeleteBranch(int branchId)
        {
            var branch = _db.Branches.FirstOrDefault(x => x.Branch_Id == branchId);

            if (branch == null) return null;

            _db.Branches.DeleteOnSubmit(branch);

            return (BranchModel) _modelFactory.CreateBranchModel(branch);
        }
        public BranchModel AddBranch(BranchModel branchModel)
        {
            var branch = new Branch()
            {
                Name = branchModel.Name,
                Code = branchModel.Code,
                Phone = branchModel.Phone,
                Email = branchModel.Email,
                Address_Id = branchModel.AddressId,
                Last_Update = branchModel.LastUpdate,
                Initial_Date = branchModel.InitialDate
            };

            _db.Branches.InsertOnSubmit(branch);
            _db.SubmitChanges();

            return (BranchModel) _modelFactory.CreateBranchModel(branch);
        }
    }
}