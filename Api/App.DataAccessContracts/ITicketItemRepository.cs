using App.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.DataAccessContracts
{
    public interface ITicketItemRepository
    {
        List<TicketItem> GetAllForTicket(int ticketId, int userId);
        TicketItem GetById(int ticketItemId);
        void Add(TicketItem ticketItem);
        void Delete(int ticketItemId);
    }
}
