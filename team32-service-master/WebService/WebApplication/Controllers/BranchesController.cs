using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication.Code;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [RoutePrefix("api/v1/branches")]
    public class BranchesController : ApiController
    {
        private readonly DatabaseHelper _databaseHelper;
        
        public BranchesController(): this(new DatabaseHelper()) { }

        public BranchesController(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        // GET: api/v1/branches
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var branches = await _databaseHelper.GetBranches();
            if (branches.Count != 0) return Ok(branches);
            return Content(HttpStatusCode.NotFound, "No Branches found.");
        }

        // GET: api/v1/branches/5
        [Route("{branchId}")]
        public async Task<IHttpActionResult> Get(int branchId)
        {
            var branch = await _databaseHelper.GetBranch(branchId);
            if (branch != null) return Ok(branch);
            return Content(HttpStatusCode.NotFound, $"Branch with id: {branchId} is not found");
        }

        // POST: api/v1/branches
        [Route("")]
        public async Task<IHttpActionResult> Post(BranchModel branchModel)
        {
            var branch = await _databaseHelper.AddBranch(branchModel);
            if (branch != null) return Ok(branch);
            return Content(HttpStatusCode.BadRequest, $"Branch: {branchModel} not added");
        }

        // PUT: api/v1/branches/5
        [Route("{branchId}")]
        public async Task<IHttpActionResult> Put(int branchId, BranchModel branchModel)
        {
            var branch = await _databaseHelper.UpdateBranch(branchId, branchModel);
            if (branch != null) return Ok(branch);
            return Content(HttpStatusCode.BadRequest, $"Could not update branch with id: {branchId}");
        }

        // DELETE: api/v1/branches/5
        [Route("{branchId}")]
        public async  Task<IHttpActionResult> Delete(int branchId)
        {
            var branch = await _databaseHelper.DeleteBranch(branchId);
            if (branch != null) return Ok(branch);
            return Content(HttpStatusCode.BadRequest, $"Branch with id: {branchId} is not deleted");
        }
    }
}
