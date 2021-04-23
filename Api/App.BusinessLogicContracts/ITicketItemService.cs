using App.Entities;
using App.Entities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.BusinessLogicContracts
{
    public interface ITicketItemService
    {
        MethodResult<List<TicketItem>> GetTicketItemsForTicket(int ticketId, int userId);

        MethodResult<TicketItem> GetById(int ticketItemId);

        MethodResult<TicketItem> AddTicketItem(string description, double price, int ticketId);

        VoidResult DeleteTicketItem(int id);
    }
}
