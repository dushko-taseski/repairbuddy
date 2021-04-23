using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Entities
{
    public class TicketItem : BaseEntity
    {
        public string Description { get; set; }

        public double Price { get; set; }

        public int TicketId { get; set; }

        public Ticket Ticket { get; set; }

    }
}
