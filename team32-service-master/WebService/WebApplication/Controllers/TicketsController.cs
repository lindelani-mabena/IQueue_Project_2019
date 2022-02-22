using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication.Code;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [RoutePrefix("api/v1/tickets")]
    public class TicketsController : ApiController
    {
        private readonly DatabaseHelper _databaseHelper;
        public TicketsController(): this(new DatabaseHelper()) { }

        public TicketsController(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }
        // GET: api/v1/tickets
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var tickets = await _databaseHelper.GetTickets();
            if (tickets.Count > 0) return Ok(tickets);
            return Content(HttpStatusCode.NotFound, "No tickets found");
        }

        // GET: api/v1/tickets/5
        [Route("{ticketId}")]
        public async Task<IHttpActionResult> Get(int ticketId)
        {
            var ticket = await _databaseHelper.GetTicket(ticketId);
            if (ticket != null) return Ok(ticket);
            return Content(HttpStatusCode.NotFound, $"Could not find ticket with id: {ticketId}");
        }

        // POST: api/v1/tickets
        [Route("")]
        public async Task<IHttpActionResult> Post(TicketModel ticketModel)
        {
            var ticket = await _databaseHelper.AddTicket(ticketModel);
            if (ticket != null) return Ok(ticket);
            return Content(HttpStatusCode.BadRequest, $"Could not add ticket: {ticketModel}");
        }

        // PUT: api/v1/tickets/5
        [Route("{ticketId}")]
        public async Task<IHttpActionResult> Put(int ticketId, TicketModel ticketModel)
        {
            var ticket = await _databaseHelper.UpdateTicket(ticketId, ticketModel);
            if (ticket != null) return Ok(ticket);
            return Content(HttpStatusCode.BadRequest,
                $"Could not update ticket with id: {ticketId} by ticket: {ticketModel}");
        }

        // DELETE: api/v1/tickets/5
        [Route("{ticketId}")]
        public async Task<IHttpActionResult> Delete(int ticketId)
        {
            var ticket = await _databaseHelper.DeleteTicket(ticketId);
            if (ticket != null) return Ok(ticket);
            return Content(HttpStatusCode.BadRequest, $"Could not delete ticket with id: {ticketId}");
        }
    }
}
