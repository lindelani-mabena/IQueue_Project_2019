using System;
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
    public class ConsultantsController : ApiController
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        // GET: api/Consultants
        [Authorize(Roles = "admin, manager")]
        [Route("Consultants")]
        public async Task<IHttpActionResult> GetConsultants()
        {
            var consultants = (from consultant in await _db.Consultants.ToListAsync() select new ConsultantModel(consultant)).Cast<UserModel>().ToList();

            return Ok(consultants);
        }
        // GET: api/Consultants/5
        [Authorize(Roles = "admin, manager, consultant")]
        [ResponseType(typeof(Consultant))]
        [Route("Consultants/{id}")]
        public async Task<IHttpActionResult> GetConsultant(string id)
        {
            var consultant = await _db.Consultants.FindAsync(id);
            
            if (consultant == null)
            {
                return NotFound();
            }

            return Ok(consultant);
        }
        // GET: api/Consultant/Profile
        [Authorize(Roles = "consultant")]
        [Route("Consultant/Profile")]
        [ResponseType(typeof(ConsultantModel))]
        public async Task<IHttpActionResult> GetProfile()
        {
            var consultant = await _db.Consultants.FindAsync(User.Identity.GetUserId());

            if (consultant == null)
            {
                return NotFound();
            }

            return Ok(new ConsultantModel(consultant));
        }

        // GET: api/Consultant/Services
        [Authorize(Roles = "consultant")]
        [Route("Consultant/Services")]
        [ResponseType(typeof(List<Service>))]
        public async Task<IHttpActionResult> GetServices()
        {
            var consultant = await _db.Consultants.FindAsync(User.Identity.GetUserId());

            if (consultant == null)
            {
                return NotFound();
            }

            return Ok(consultant.Branch.Services);
        }

        // PUT: api/Consultant/Profile/Update
        [Authorize(Roles = "consultant")]
        [Route("Consultant/Profile/Update")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpdateProfile(ConsultantModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var consultant = await _db.Consultants.FindAsync(User.Identity.GetUserId());

            if (consultant == null)
            {
                return NotFound();
            }

            consultant.FirstName = model.FirstName;
            consultant.LastName = model.LastName;
            consultant.Title = model.Title;
            consultant.PhoneNumber = model.PhoneNumber;
            consultant.LastUpdate = DateTime.Now;

            _db.Entry(consultant).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultantExists(User.Identity.GetUserId()))
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

        // GET: api/Consultant/Tickets/4/Consultation/Start/Services/4
        [Authorize(Roles = "consultant")]
        [Route("Consultant/Ticket/Consultation/Start/Services/{serviceId}")]
        [ResponseType(typeof(Ticket))]
        public async Task<IHttpActionResult> StartTicketConsultation(int serviceId)
        {
            var ticket = await _db.Tickets.FirstOrDefaultAsync(x => x.Service.Id == serviceId && x.Active && x.StartAt == TimeSpan.Zero);

            if (ticket == null)
            {
                return NotFound();
            }

            /*
            ticket.ConsultantId = User.Identity.GetUserId();
            ticket.LastUpdate = DateTime.Now;
            ticket.Active = true;
            ticket.Status = "Consultation";
            ticket.StartAt = DateTime.Now.TimeOfDay;
            ticket.Duration = TimeSpan.Zero;
            */

            var consultant = await _db.Consultants.FindAsync(User.Identity.GetUserId());

            if (consultant == null)
            {
                return BadRequest();
            }

            ticket.Consult(consultant, "Consultation", true);

            _db.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               //if (!TicketExists(id))
                //{
                    return NotFound();
                //}
                //else
                //{
                    throw;
                //}
            }

            return Ok(ticket);
        }
        // GET: api/Consultant/Tickets/4/Consultation/End
        [Authorize(Roles = "consultant")]
        [Route("Consultant/Ticket/Consultation/End/Services/{serviceId}")]
        [ResponseType(typeof(Ticket))]
        public async Task<IHttpActionResult> EndTicketConsultation(int serviceId)
        {
            string userId = User.Identity.GetUserId();

            var ticket = await _db.Tickets.FirstOrDefaultAsync(x => x.Service.Id == serviceId && x.Active && x.EndAt == TimeSpan.Zero && x.Consultant.Id == userId);

            if (ticket == null)
            {
                return NotFound();
            }

            var consultant = await _db.Consultants.FindAsync(User.Identity.GetUserId());

            if (consultant == null)
            {
                return NotFound();
            }

            ticket.Consult(consultant, "Served", false);

            _db.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!TicketExists(id))
                //{
                    return NotFound();
                //}
                //else
                //{
                    throw;
                //}
            }

            return Ok(ticket);
        }

        // GET: api/Consultant/Tickets/4/Consultation/End/Comment/CommentMessage
        [Authorize(Roles = "consultant")]
        [Route("Consultant/Ticket/Consultation/End/Services/{serviceId}/Comments/{comments}")]
        [ResponseType(typeof(Ticket))]
        public async Task<IHttpActionResult> EndTicketConsultationNew(int serviceId, string comments)
        {
            string userId = User.Identity.GetUserId();

            var ticket = await _db.Tickets.FirstOrDefaultAsync(x => x.Service.Id == serviceId && x.Active && x.EndAt == TimeSpan.Zero && x.Consultant.Id == userId);

            if (ticket == null)
            {
                return NotFound();
            }

            var consultant = await _db.Consultants.FindAsync(User.Identity.GetUserId());

            if (consultant == null)
            {
                return NotFound();
            }

            ticket.Consult(consultant, "Served", false);

            var consultation = new Consultation(comments) {Ticket = ticket};

            consultant.Consultations.Add(consultation);

            _db.Entry(ticket).State = EntityState.Modified;
            //_db.Entry(consultant).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!TicketExists(id))
                //{
                return NotFound();
                //}
                //else
                //{
                throw;
                //}
            }

            return Ok(ticket);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
        private bool ConsultantExists(string id)
        {
            return _db.Consultants.Count(e => e.Id == id) > 0;
        }
        private bool TicketExists(int id)
        {
            return _db.Tickets.Count(e => e.Id == id) > 0;
        }
    }
}