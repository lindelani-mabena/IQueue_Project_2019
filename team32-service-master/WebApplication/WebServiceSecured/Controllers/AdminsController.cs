using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebServiceSecured.Models.Account;
using WebServiceSecured.Models.Domain;
using WebServiceSecured.Models.IdentityModels.DataContext;

namespace WebServiceSecured.Controllers
{
    [RoutePrefix("api")]
    public class AdminsController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: api/Admins
        [Authorize(Roles = "admin")]
        public List<UserModel> GetAdmins()
        {
            var admins = new List<UserModel>();

            _db.Admin.ToList().ForEach(x =>
                admins.Add(new UserModel(x.Id, x.FirstName, x.LastName,
                    x.Title, x.Email, x.PhoneNumber, x.InitialDate,
                    x.LastUpdate, x.DateAssigned, x.Addresses)));

            return admins;
        }

        // GET: api/Admin/Profile
        [Authorize(Roles = "admin")]
        [Route("Admin/Profile")]
        [ResponseType(typeof(AdminModel))]
        public async Task<IHttpActionResult> GetProfile()
        {
            var admin = await _db.Admin.FindAsync(User.Identity.GetUserId());

            if (admin == null)
            {
                return NotFound();
            }

            return Ok(new AdminModel(admin));
        }

        // PUT: api/Consultant/Profile/Update
        [Authorize(Roles = "admin")]
        [Route("Admin/Profile/Update")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpdateProfile(AdminModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var admin = await _db.Admin.FindAsync(User.Identity.GetUserId());

            if (admin == null)
            {
                return NotFound();
            }

            admin.FirstName = model.FirstName;
            admin.LastName = model.LastName;
            admin.Title = model.Title;
            admin.PhoneNumber = model.PhoneNumber;
            admin.LastUpdate = DateTime.Now;

            _db.Entry(admin).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(User.Identity.GetUserId()))
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

        // GET: api/Admins/5
        [Authorize(Roles = "admin")]
        [ResponseType(typeof(Admin))]
        public async Task<IHttpActionResult> GetAdmin(string id)
        {
            Admin admin = await _db.Admin.FindAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            return Ok(admin);
        }

        

        // PUT: api/Admins/Branch/4/Assign/Manager/3
        [Authorize(Roles = "admin")]
        [HttpPut]
        [Route("Branch/{branchId}/Assign/Manager/{managerId}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutManager(int branchId, string managerId)
        {
            var branch = await _db.Branches.FindAsync(branchId);

            if (branch == null)
            {
                return NotFound();
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
                if (!AdminExists(User.Identity.GetUserId()))
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

        // PUT: api/Admin/Assign/3
        [Authorize(Roles = "admin")]
        [HttpPut]
        [Route("Assign/{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Assign(string id)
        {
            var client = await _db.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            client.Claims.Add(new IdentityUserClaim()
            {
                ClaimValue = "admin",
                ClaimType = ClaimTypes.Role
            });

            _db.Entry(client).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(User.Identity.GetUserId()))
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

        // PUT: api/Admin/Assign/Manager/3
        [Authorize(Roles = "admin")]
        [HttpPut]
        [Route("Assign/Manager/{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> AssignManager(string id)
        {
            var client = await _db.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            client.Claims.Add(new IdentityUserClaim() 
            { 
                ClaimValue = "manager",
                ClaimType = ClaimTypes.Role
            });

            _db.Entry(client).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(User.Identity.GetUserId()))
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

        // PUT: api/Admin/Branch/4/UnAssign/Manager
        [Authorize(Roles = "admin")]
        [HttpPut]
        [Route("UnAssign/Manager/{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UnAssignManager(string id)
        {
            var manager = _db.Clients.Find(id);

            if (manager == null)
            {
                return NotFound();
            }

            manager.Claims.Remove(manager.Claims.Last());

            _db.Entry(manager).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(User.Identity.GetUserId()))
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

        // POST: api/Admins
        [ResponseType(typeof(Admin))]
        public async Task<IHttpActionResult> PostAdmin(Admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Users.Add(admin);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AdminExists(admin.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = admin.Id }, admin);
        }

        // DELETE: api/Admins/5
        [ResponseType(typeof(Admin))]
        public async Task<IHttpActionResult> DeleteAdmin()
        {
            Admin admin = await _db.Admin.FindAsync(User.Identity.GetUserId());
            if (admin == null)
            {
                return NotFound();
            }

            _db.Users.Remove(admin);
            await _db.SaveChangesAsync();

            return Ok(admin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdminExists(string id)
        {
            return _db.Admin.Count(e => e.Id == id) > 0;
        }
    }
}