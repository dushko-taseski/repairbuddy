using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Web.Dtos.Assets
{
    public class NewAssetDto
    {
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
