using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Entities
{
    public class Ticket : BaseEntity
    {
        public string Description { get; set; }

        public DateTime DateOpen { get; set; }

        public TicketStatus TicketStatus { get; set; }

        public DateTime? DateClosed { get; set; }

        public int AssetId { get; set; }

        public Asset Asset { get; set; }

        public List<TicketItem> Items { get; set; }

        public double TotalPrice => Items?.Select(x => x.Price).DefaultIfEmpty(0).Sum() ?? 0.0;

    }
}
