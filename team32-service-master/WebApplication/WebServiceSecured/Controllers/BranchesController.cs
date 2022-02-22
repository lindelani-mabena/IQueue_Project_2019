using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using WebServiceSecured.Code;
using WebServiceSecured.Models.Domain;
using WebServiceSecured.Models.IdentityModels.DataContext;

namespace WebServiceSecured.Controllers
{
    [RoutePrefix("api/branches")]
    public class BranchesController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: api/Branches
        [Route("")]
        public List<Branch> GetBranches()
        {
            return _db.Branches.ToList();
        }

        // GET: api/Branches/5
        [Route("{id}", Name = "GetBranchById")]
        [ResponseType(typeof(Branch))]
        public async Task<IHttpActionResult> GetBranch(int id)
        {
            Branch branch = await _db.Branches.FindAsync(id);
            if (branch == null)
            {
                return NotFound();
            }

            return Ok(branch);
        }

        [Authorize(Roles = "admin, manager")]
        // PUT: api/Branches/5
        [Route("{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBranch(int id, Branch model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var branch = await _db.Branches.FindAsync(id);

            if (branch == null)
            {
                return NotFound();
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            branch.Copy(model);
            branch.LastUpdate = DateTime.Now;

            _db.Entry(branch).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT: api/Branches/5
        [Authorize(Roles = "admin, manager")]
        [Route("{id}/Assign/Consultant/{consultantId}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> AssignConsultant(int id, string consultantId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var branch = await _db.Branches.FindAsync(id);

            if (branch == null)
            {
                return BadRequest();
            }

            var consultant = await _db.Consultants.FindAsync(consultantId);

            if (consultant == null)
            {
                return NotFound();
            }

            consultant.AssignedAsConsultant = true;
            consultant.Branch = branch;
            branch.Consultants.Add(consultant);

            _db.Entry(branch).State = EntityState.Modified;
            _db.Entry(consultant).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT: api/Branches/5
        [Authorize(Roles = "admin, manager")]
        [Route("{id}/UnAssign/Consultant/{consultantId}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UnAssignConsultant(int id, string consultantId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var branch = await _db.Branches.FindAsync(id);

            if (branch == null)
            {
                return BadRequest();
            }

            var consultant = await _db.Consultants.FindAsync(consultantId);

            if (consultant == null)
            {
                return NotFound();
            }

            branch.Consultants.Remove(consultant);
            consultant.Branch = null;

            _db.Entry(branch).State = EntityState.Modified;
            _db.Entry(consultant).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT: api/Branches/5
        [Authorize(Roles = "admin")]
        [Route("{id}/Assign/Manager/{managerId}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> AssignManager(int id, string managerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var branch = await _db.Branches.FindAsync(id);

            if (branch == null)
            {
                return BadRequest();
            }

            var manager = await _db.Managers.FindAsync(managerId);

            if (manager == null)
            {
                return NotFound();
            }

            branch.Manager = manager;

            _db.Entry(branch).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT: api/Branches/5
        [Authorize(Roles = "admin")]
        [Route("{id}/UnAssign/Manager/{managerId}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UnAssignManager(int id, string managerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var branch = await _db.Branches.FindAsync(id);

            if (branch == null)
            {
                return BadRequest();
            }

            var manager = await _db.Managers.FindAsync(managerId);

            if (manager == null)
            {
                return NotFound();
            }

            branch.Manager = null;

            _db.Entry(branch).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT: api/Branches/5
        [Authorize(Roles = "admin, manager")]
        [Route("{id}/Assign/Service/{serviceId}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> AssignService(int id, int serviceId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var branch = await _db.Branches.FindAsync(id);

            if (branch == null)
            {
                return BadRequest();
            }

            var service = await _db.Services.FindAsync(serviceId);

            if (service == null)
            {
                return NotFound();
            }

            branch.Services.Add(service);
            service.Branches.Add(branch);

            _db.Entry(branch).State = EntityState.Modified;
            _db.Entry(service).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(id))
                {
                    return NotFound();
                } 
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT: api/Branches/5
        [Authorize(Roles = "admin, manager")]
        [Route("{id}/UnAssign/Service/{serviceId}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UnAssignService(int id, int serviceId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var branch = await _db.Branches.FindAsync(id);

            if (branch == null)
            {
                return BadRequest();
            }

            var service = await _db.Services.FindAsync(serviceId);

            if (service == null)
            {
                return NotFound();
            }

            branch.Services.Remove(service);
            service.Branches.Remove(branch);

            _db.Entry(branch).State = EntityState.Modified;
            _db.Entry(service).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT: api/Branches/5
        [Authorize(Roles = "admin, manager")]
        [Route("{branchId}/Add/Consultant/{consultantId}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> AddConsultant(int branchId, int consultantId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var branch = await _db.Branches.FindAsync(branchId);

            if (branch == null)
            {
                return BadRequest();
            }

            var consultant = await _db.Consultants.FindAsync(consultantId);

            if (consultant == null)
            {
                return NotFound();
            }

            branch.Consultants.Add(consultant);

            _db.Entry(branch).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(branchId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT: api/Branches/5/UpdateAddress
        [Authorize(Roles = "admin, manager")]
        [Route("{id}/UpdateAddress")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBranchAddress(int id, Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var branch = await _db.Branches.FindAsync(id);

            if (branch == null)
            {
                return NotFound();
            }

            if (branch.Address != null)
            {
                branch.Address.Address1 = address.Address1;
                branch.Address.Address2 = address.Address2;
                branch.Address.City = address.City;
                branch.Address.Province = address.Province;
                branch.Address.Zip = address.Zip;
            }
            else
            {
                branch.Address = address;
            }

            _db.Entry(branch).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT: api/Branches/5/DeleteAddress
        [Authorize(Roles = "admin")]
        [HttpPut]
        [Route("{id}/DeleteAddress")]
        [ResponseType(typeof(Address))]
        public async Task<IHttpActionResult> DeleteAddress(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var branch = await _db.Branches.FindAsync(id);

            if (branch == null)
            {
                return NotFound();
            }

            Address address = branch.Address;

            if (address == null)
            {
                return NotFound();
            }

            _db.Addresses.Remove(address);
            await _db.SaveChangesAsync();

            _db.Entry(branch).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(address);
        }

        // POST: api/Branches
        [Authorize(Roles = "admin, manager")]
        [Route("")]
        [ResponseType(typeof(Branch))]
        public async Task<IHttpActionResult> PostBranch(Branch branch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (User.IsInRole("admin"))
            {
                var admin = await _db.Admin.FindAsync(User.Identity.GetUserId());
                branch.Admin = admin;
            } else if (User.IsInRole("manager"))
            {
                var manager = await _db.Managers.FindAsync(User.Identity.GetUserId());
                branch.Manager = manager;
            }

            branch.Address.InitialDate = DateTime.Now;
            branch.Address.LastUpdate = DateTime.Now;

            branch.InitialDate = DateTime.Now;
            branch.LastUpdate = DateTime.Now;

            var destination_latLong = Maps.GetLatLongByAddress($"{ branch.Address.Address1 }, { branch.Address.Address2 }, { branch.Address.City }, { branch.Address.Province }, {branch.Address.Province }");

            var lattitude = Convert.ToString(destination_latLong.results[0].geometry.location.lat, CultureInfo.InvariantCulture);

            var longitude = Convert.ToString(destination_latLong.results[0].geometry.location.lng, CultureInfo.InvariantCulture);

            branch.Address.Latitude = lattitude;
            branch.Address.Longitude = longitude;

            _db.Branches.Add(branch);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetBranchById", new { id = branch.Id }, branch);
        }

        // DELETE: api/Branches/5
        [Authorize(Roles = "admin, manager")]
        [Route("")]
        [ResponseType(typeof(Branch))]
        public async Task<IHttpActionResult> DeleteBranch(int id)
        {
            var branch = await _db.Branches.FindAsync(id);

            if (branch == null)
            {
                return NotFound();
            }

            _db.Branches.Remove(branch);
            await _db.SaveChangesAsync();

            return Ok(branch);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BranchExists(int id)
        {
            return _db.Branches.Count(e => e.Id == id) > 0;
        }
    }
}