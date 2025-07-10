using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModels
{
    public class OrderRequestDto
    {
        public string BasketId { get; set; }
        public AddressDto ShipToAdderss { get; set; }
        public int DeliveryMethodId { get; set; }
    }
}
