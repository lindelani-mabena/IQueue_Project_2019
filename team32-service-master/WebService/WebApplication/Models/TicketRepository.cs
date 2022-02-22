using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models.Abstraction;

namespace WebApplication.Models
{
    public class TicketRepository: ITicketRepository
    {
        private readonly IqueueDbDataContext _db;
        private readonly IModelFactory _modelFactory;

        public TicketRepository(IqueueDbDataContext db)
        {
            _db = db;
            _modelFactory = new ModelFactory(_db);
        }

        public List<TicketModel> Tickets()
        {
            var ticketModels = new List<TicketModel>();
            var tickets = _db.Tickets;

            foreach (var ticket in tickets)
            {
                ticketModels.Add((TicketModel)
                    _modelFactory.CreateTicketModel(ticket));
            }

            return ticketModels;
        }

        public TicketModel GetTicket(int ticketId)
        {
            var ticket = _db.Tickets.FirstOrDefault(x => x.Ticket_Id == ticketId);
            if (ticket == null) return null;
            return (TicketModel)_modelFactory.CreateTicketModel(ticket);
        }

        public TicketModel UpdateTicket(int ticketId, TicketModel ticketModel)
        {
            var ticket = _db.Tickets.FirstOrDefault(x => x.Ticket_Id == ticketId);

            if (ticket == null) return null;

            ticket.User_Id = ticketModel.UserId;
            ticket.Queue_Id = ticketModel.QueueId;
            ticket.Consultation_Id = ticketModel.ConsultationId;
            ticket.Token = ticketModel.Token;
            if (ticket.Average_Waiting_Time != null) ticket.Average_Waiting_Time = (TimeSpan) ticketModel.AverageWaitingTime;
            ticket.Status = ticketModel.Status;
            ticket.Type = ticketModel.Type;
            ticket.Last_Update = ticketModel.GetLastUpdateDate();

            _db.SubmitChanges();

            return (TicketModel)_modelFactory.CreateTicketModel(ticket);
        }

        public TicketModel AddTicket(TicketModel ticketModel)
        {
            var ticket = new Ticket()
            {
                User_Id = ticketModel.UserId,
                Queue_Id = ticketModel.QueueId,
                Consultation_Id = ticketModel.ConsultationId,
                Token = ticketModel.Token,
                Average_Waiting_Time = ticketModel.AverageWaitingTime,
                Status = ticketModel.Status,
                Type = ticketModel.Type,
                Last_Update = ticketModel.LastUpdate,
                Initial_Date = ticketModel.InitialDate
            };

            _db.Tickets.InsertOnSubmit(ticket);
            _db.SubmitChanges();

            return (TicketModel)_modelFactory.CreateTicketModel(ticket);
        }

        public TicketModel DeleteTicket(int ticketId)
        {
            var ticket = _db.Tickets.FirstOrDefault(x => x.Ticket_Id == ticketId);

            if (ticket == null) return null;

            _db.Tickets.DeleteOnSubmit(ticket);
            _db.SubmitChanges();

            return (TicketModel)_modelFactory.CreateTicketModel(ticket);
        }
    }
}