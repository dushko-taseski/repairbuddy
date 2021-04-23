using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Dtos.Tickets
{
    public class NewTicketItemDto
    {
        public string Description { get; set; }
        public double Price { get; set; }
        public int TicketId { get; set; }
    }
}
