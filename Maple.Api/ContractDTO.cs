using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maple.Api
{
    public class ContractDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
