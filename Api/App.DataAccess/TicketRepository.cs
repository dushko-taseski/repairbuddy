using App.DataAccessContracts;
using App.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.DataAccess
{
    public class TicketRepository : ITicketRepository
    {
        private readonly DataBaseContext _context;

        public TicketRepository(DataBaseContext context)
        {
            _context = context;
        }
        public void Add(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
        }

        public void Update(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            _context.SaveChanges();
        }

        public void Delete(int ticketId)
        {
            var result = _context.Tickets.FirstOrDefault(x => x.Id == ticketId);
            if (result != null)
            {
                _context.Remove(result);
                _context.SaveChanges();
            }
        }

        public List<Ticket> GetAllForUser(int userId)
        {
            return _context.Tickets.Include(x => x.Items).Include(x => x.Asset).ThenInclude(x => x.Customer)
                .Where(x => x.Asset.Customer.UserId == userId).ToList();
        }

        public Ticket GetById(int ticketId)
        {
            return _context.Tickets.FirstOrDefault(x => x.Id == ticketId);
        }

    }
}
