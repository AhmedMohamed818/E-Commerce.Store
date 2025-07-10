using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class OrderResultDto
    {

        public Guid Id { get; set; }
        // public int Id { get; set; }
        public string UserEmail { get; set; }
        public AddressDto ShippingAddress { get; set; }
        public ICollection<OrderItemDto> orderItem { get; set; } = new List<OrderItemDto>(); 
        public string DeliveryMethod { get; set; }
        
        // payment status
        public string PaymentStatus { get; set; } 

        //sub total of the order
        public decimal SubTotal { get; set; }
        // order date 
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string PaymentIntentId { get; set; } = string.Empty;
        public decimal Total { get; set; }
    }
}
