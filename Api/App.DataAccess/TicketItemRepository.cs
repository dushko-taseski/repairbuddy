using App.DataAccessContracts;
using App.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.DataAccess
{
    public class TicketItemRepository : ITicketItemRepository
    {
        private readonly DataBaseContext _context;

        public TicketItemRepository(DataBaseContext context)
        {
            _context = context;
        }

        public void Add(TicketItem ticketItem)
        {
            _context.TicketItem.Add(ticketItem);
            _context.SaveChanges();
        }

        public void Delete(int ticketItemId)
        {
            var result = _context.TicketItem.FirstOrDefault(x => x.Id == ticketItemId);
            if (result != null)
            {
                _context.Remove(result);
                _context.SaveChanges();
            }
        }

        public List<TicketItem> GetAllForTicket(int ticketId, int userId)
        {
            return _context.TicketItem.Include(x => x.Ticket).ThenInclude(x => x.Asset).ThenInclude(x => x.Customer)
                .Where(x => x.TicketId == ticketId && x.Ticket.Asset.Customer.UserId == userId).ToList();
        }

        public TicketItem GetById(int ticketItemId)
        {
            return _context.TicketItem.FirstOrDefault(x => x.Id == ticketItemId);
        }
    }
}
