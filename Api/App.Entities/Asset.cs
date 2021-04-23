using System.Collections.Generic;

namespace App.Entities
{
    public class Asset : BaseEntity
    {
        public int CustomerId { get; set; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Customer Customer { get; set; }

    }
}
