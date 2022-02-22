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
using WebGrease.Css.Extensions;
using WebServiceSecured.Models.Domain;
using WebServiceSecured.Models.IdentityModels.DataContext;

namespace WebServiceSecured.Controllers
{
    public class ServicesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Services
        public async Task<List<Service>> GetServices()
        {
            return await db.Services.ToListAsync();
        }

        // GET: api/Services/5
        [ResponseType(typeof(Service))]
        public async Task<IHttpActionResult> GetService(int id)
        {
            var service = await db.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        // PUT: api/Services/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutService(int id, Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != service.Id)
            {
                return BadRequest();
            }

            service.LastUpdate = DateTime.Now;

            db.Entry(service).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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

        // PUT: api/Services/5/Join/Branches/2
        [Route("api/Services/{id}/Join/Branches/{branchId}")]
        [ResponseType(typeof(Ticket))]
        public async Task<IHttpActionResult> JoinService(int id, int branchId)
        {
            var service = await db.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            var branch = await db.Branches.FindAsync(branchId);

            if (branch == null)
            {
                return NotFound();
            }

            var client = await db.Clients.FindAsync(User.Identity.GetUserId());

            Ticket ticket = null;

            if (client == null)
            {
                var ticketNumber =
                    service.Tickets.Count(x => x.InitialDate.Date == DateTime.Now.Date);
                /*
                var ticket = new Ticket()
                {
                    Token = "Ticket " + (ticketNumber < 10 ? $"0{ticketNumber}" : $"{ticketNumber}"),
                    Active = true,
                    InitialDate = DateTime.Now,
                    Status = "Pending",
                    Type = "Unregistered",
                    StartAt = TimeSpan.Zero,
                    EndAt = TimeSpan.Zero,
                    Duration = TimeSpan.Zero,
                    EstimatedWaitingTime = averageTime
                };
                */

                ticket = new Ticket();
                ticket.Join(ticketNumber, branch, service, "Unregistered");
                ticket.EstimatedWaitingTime = service.CalculateAverageWaitingTime();

                service.Tickets.Add(ticket);
            }
            else
            {
                ticket = client.Tickets.FirstOrDefault(x => x.Active);

                if (ticket == null)
                {
                    var ticketNumber =
                        service.Tickets.Count(x => x.InitialDate.Date == DateTime.Now.Date);

                    /*
                    ticket = new Ticket()
                    {
                        Token = "Online Ticket " + (ticketNumber < 10 ? $"0{ticketNumber}" : $"{ticketNumber}"),
                        Active = true,
                        InitialDate = DateTime.Now,
                        EstimatedWaitingTime = averageTime,
                        StartAt = TimeSpan.Zero,
                        EndAt = TimeSpan.Zero,
                        Duration = TimeSpan.Zero,
                        Type = "Registered",
                        Status = "Pending",
                    };
                    */
                    ticket = new Ticket();
                    ticket.Join(ticketNumber, branch, service, "Registered");
                    ticket.EstimatedWaitingTime = service.CalculateAverageWaitingTime();

                    client.Tickets.Add(ticket);

                    db.Entry(client).State = EntityState.Modified;
                }

                service.Tickets.Add(ticket);
            }

            db.Entry(service).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(ticket);
        }

        // PUT: api/Services/5/Join/Branches/2/mobile
        [Route("api/Services/{id}/Join/Branches/{branchId}/mobile")]
        [Authorize(Roles = "client")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> JoinServiceMobile(int id, int branchId)
        {
            var service = await db.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            var branch = await db.Branches.FindAsync(branchId);

            if (branch == null)
            {
                return NotFound();
            }

            var client = await db.Clients.FindAsync(User.Identity.GetUserId());

            if (client == null)
            {
                return NotFound();
            }

            var totalTime = TimeSpan.Zero;

            service.Tickets.Where(x => x.InitialDate.Date == DateTime.Now.Date).ForEach(x => { totalTime += x.Duration; });

            var averageTime = TimeSpan.Zero;

            if (!totalTime.Equals(TimeSpan.Zero))
            {
                var numberTickets = service.Tickets.Count(x => x.InitialDate.Date == DateTime.Now.Date && x.Active);

                if (numberTickets != 0)
                {
                    averageTime = TimeSpan.FromTicks(totalTime.Ticks /
                                                     service.Tickets.Count(x => x.InitialDate.Date == DateTime.Now.Date && x.Active));
                }
            }

            var ticket = client.Tickets.FirstOrDefault(x => x.Active);

            if (ticket == null)
            {
                var ticketNumber =
                    service.Tickets.Count(x => x.InitialDate.Date == DateTime.Now.Date);

                ticket = new Ticket();
                ticket.Join(ticketNumber, branch, service, "Mobile");
                ticket.EstimatedWaitingTime = averageTime;

                client.Tickets.Add(ticket);

                db.Entry(client).State = EntityState.Modified;
            }

            service.Tickets.Add(ticket);

            db.Entry(service).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(ticket);
        }

        // POST: api/Services
        [ResponseType(typeof(Service))]
        public async Task<IHttpActionResult> PostService(Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Service s = service;
            s.LastUpdate = DateTime.Now;
            s.InitialDate = DateTime.Now;

            db.Services.Add(s);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = s.Id }, s);
        }

        // DELETE: api/Services/5
        [ResponseType(typeof(Service))]
        public async Task<IHttpActionResult> DeleteService(int id)
        {
            var service = await db.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            db.Services.Remove(service);
            await db.SaveChangesAsync();

            return Ok(service);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServiceExists(int id)
        {
            return db.Services.Count(e => e.Id == id) > 0;
        }
    }
}