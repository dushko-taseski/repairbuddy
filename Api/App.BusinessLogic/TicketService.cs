using App.BusinessLogicContracts;
using App.DataAccessContracts;
using App.Entities;
using App.Entities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.BusinessLogic
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public MethodResult<Ticket> AddTicket(string description, int assetId)
        {
            var newTicket = new Ticket
            {
                Description = description,
                DateOpen = DateTime.Now,
                TicketStatus = TicketStatus.Open,
                DateClosed = null,
                AssetId = assetId
            };

            _ticketRepository.Add(newTicket);

            return new MethodResult<Ticket>(newTicket);
        }

        public MethodResult<Ticket> UpdateTicket(int ticketId)
        {
            var ticket = _ticketRepository.GetById(ticketId);
            if(ticket != null)
            {
                ticket.TicketStatus = TicketStatus.Closed;
                ticket.DateClosed = DateTime.Now;
            }

            _ticketRepository.Update(ticket);

            return new MethodResult<Ticket>(ticket);
        }

        public VoidResult DeleteTicket(int id)
        {
            var deleteTicket = _ticketRepository.GetById(id);
            if (deleteTicket != null)
            {
                _ticketRepository.Delete(id);
                return VoidResult.Ok();
            }
            else
            {
                return VoidResult.CreateFaultedResult("The ticket can't be found.");
            }
            throw new NotImplementedException();
        }

        public MethodResult<List<Ticket>> GetAllTicketsForUser(int userId)
        {
            var tickets = _ticketRepository.GetAllForUser(userId);
            return new MethodResult<List<Ticket>>(tickets);
        }

        public MethodResult<Ticket> GetById(int ticketId)
        {
            var ticket = _ticketRepository.GetById(ticketId);
            if (ticket != null)
            {
                return new MethodResult<Ticket>(ticket);
            }

            return MethodResult<Ticket>.CreateFaultedResult("The ticket with this Id can't be found.");
        }
    }
}
