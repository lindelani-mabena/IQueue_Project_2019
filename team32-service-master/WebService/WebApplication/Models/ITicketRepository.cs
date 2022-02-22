using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public interface ITicketRepository
    {
        List<TicketModel> Tickets();
        TicketModel GetTicket(int ticketId);
        TicketModel UpdateTicket(int ticketId, TicketModel ticketModel);
        TicketModel AddTicket(TicketModel ticketModel);
        TicketModel DeleteTicket(int ticketId);
    }
}
