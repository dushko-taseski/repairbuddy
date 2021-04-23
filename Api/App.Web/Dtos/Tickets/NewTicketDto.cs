using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Dtos
{
    public class NewTicketDto
    {
        public string Description { get; set; }

        public int AssetId { get; set; }
    }
}
