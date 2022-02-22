using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using WebServiceSecured.Models.Account;
using WebServiceSecured.Models.Domain;
using WebServiceSecured.Models.IdentityModels.DataContext;

namespace WebServiceSecured.Controllers
{
    [RoutePrefix("api")]
    public class ClientsController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        // GET: api/Clients
        [Authorize(Roles = "admin, manager, consultant")]
        [Route("clients")]
        public List<UserModel> GetClients()
        {
            var clients = new List<UserModel>();

            _db.Clients.ToList().ForEach(x => clients.Add(new ClientModel(x)));

            return clients;
        }

        // GET: api/Clients/5
        [Route("Clients/{id}")]
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> GetClient(string id)
        {
            var client = await _db.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(new ClientModel(client));
        }

        // GET: api/Client/Profile
        [Route("Client/Profile")]
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> GetProfile()
        {
            var client = await _db.Clients.FindAsync(User.Identity.GetUserId());

            if (client == null)
            {
                return NotFound();
            }

            return Ok(new ClientModel(client));
        }

        // PUT: api/Client/Profile/Update
        [Authorize(Roles = "client")]
        [Route("Client/Profile/Update")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpdateProfile(ClientModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var client = await _db.Clients.FindAsync(User.Identity.GetUserId());

            if (client == null)
            {
                return NotFound();
            }

            client.FirstName = model.FirstName;
            client.LastName = model.LastName;
            client.Title = model.Title;
            client.PhoneNumber = model.PhoneNumber;
            client.LastUpdate = model.LastUpdate;

            _db.Entry(client).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(User.Identity.GetUserId()))
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

        private bool ClientExists(string id)
        {
            return _db.Users.Count(e => e.Id == id) > 0;
        }
    }
}