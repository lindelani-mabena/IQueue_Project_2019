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
using System.Web.Razor.Parser.SyntaxTree;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebServiceSecured.Models.Account;
using WebServiceSecured.Models.Domain;
using WebServiceSecured.Models.DomainModels;
using WebServiceSecured.Models.IdentityModels.DataContext;

namespace WebServiceSecured.Controllers
{
    [RoutePrefix("api")]
    public class ManagersController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: api/Managers
        [Authorize(Roles = "admin")]
        [Route("Managers")]
        [ResponseType(typeof(List<UserModel>))]
        public List<UserModel> GetManagers()
        {
            var managers = new List<UserModel>();

            _db.Managers.ToList().ForEach(x => managers.Add(new ManagerModel(x)));

            return managers;
        }

        // GET: api/Consultants
        [Authorize(Roles = "admin")]
        [Route("Managers/List")]
        public async Task<IHttpActionResult> GetManagersList()
        {
            var managers = (from manager in await _db.Managers.ToListAsync() select new ManagerModel(manager)).Cast<UserModel>().ToList();

            return Ok(managers);
        }

        // GET: api/Managers/5
        [Authorize(Roles = "admin, manager")]
        [Route("Managers/{id}")]
        [ResponseType(typeof(Manager))]
        public async Task<IHttpActionResult> GetManager(string id)
        {
            var manager = await _db.Managers.FindAsync(id);

            if (manager == null)
            {
                return NotFound();
            }

            return Ok(manager);
        }

        // GET: api/Manager/Profile
        [Authorize(Roles = "manager")]
        [Route("Manager/Profile")]
        [ResponseType(typeof(Manager))]
        public async Task<IHttpActionResult> GetProfile()
        {
            var manager = await _db.Managers.FindAsync(User.Identity.GetUserId());

            if (manager == null)
            {
                return NotFound();
            }

            return Ok(manager);
        }

        // PUT: api/Manager/Profile/Update
        [Authorize(Roles = "manager")]
        [Route("Manager/Profile/Update")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> ProfileUpdate(ManagerModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var manager = await _db.Managers.FindAsync(User.Identity.GetUserId());

            if (manager == null)
            {
                return NotFound();
            }

            manager.FirstName = model.FirstName;
            manager.LastName = model.LastName;
            manager.Title = model.Title;
            manager.PhoneNumber = model.PhoneNumber;
            manager.LastUpdate = DateTime.Now;

            _db.Entry(manager).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManagerExists(User.Identity.GetUserId()))
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ManagerExists(string id)
        {
            return _db.Managers.Count(e => e.Id == id) > 0;
        }
    }
}