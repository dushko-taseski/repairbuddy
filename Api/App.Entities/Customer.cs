﻿using System.Collections.Generic;

namespace App.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public int UserId { get; set; }

    }
}
