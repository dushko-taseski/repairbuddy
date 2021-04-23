using App.Entities;
using App.Entities.Results;
using System;
using System.Collections.Generic;


namespace App.BusinessLogicContracts
{
    public interface ITicketService
    {
        MethodResult<List<Ticket>> GetAllTicketsForUser(int userId);

        MethodResult<Ticket> GetById(int ticketId);

        MethodResult<Ticket> AddTicket(string description, int assetId);

        MethodResult<Ticket> UpdateTicket(int ticketId);

        VoidResult DeleteTicket(int id);
    }
}
