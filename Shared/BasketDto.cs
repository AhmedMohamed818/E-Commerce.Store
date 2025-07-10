using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class BasketDto
    {
        public int Id { get; set; }
        public IEnumerable<BasketItemDto> Items { get; set; }
        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }
        public int? DeliveryMetodId { get; set; }
        public decimal? ShippingPrice { get; set; }
    }
}
