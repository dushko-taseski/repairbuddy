using App.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.DataAccessContracts
{
    public interface ITicketRepository
    {
        List<Ticket> GetAllForUser(int userId);
        Ticket GetById(int ticketId);
        void Add(Ticket ticket);
        void Update(Ticket ticket);
        void Delete(int ticketId);

    }
}
