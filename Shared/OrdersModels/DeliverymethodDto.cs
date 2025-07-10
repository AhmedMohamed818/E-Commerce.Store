using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrdersModels
{
    public class DeliverymethodDto
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public int DeliveryTime { get; set; } // in days
        public decimal Cost { get; set; }
    }
}
