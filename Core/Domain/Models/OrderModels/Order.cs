using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModels
{
    public class Order : BaseEntity<Guid>
    {
        // Constructor to initialize an Order with required properties
        public Order() { }
        public Order(string userEmail, Address shippingAddress, ICollection<OrderItem> orderItem, DeliveryMethod deliveryMethod, decimal subTotal, string paymentIntentId)
        {
            Id = Guid.NewGuid(); // Ensure a new unique ID is generated for each order
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            OrderItems = orderItem;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        // public int Id { get; set; }
        public string UserEmail { get; set; }
        public Address ShippingAddress  { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); // navigational Property
        public DeliveryMethod DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }


        // payment status
        public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.Pending;

        //sub total of the order
        public decimal SubTotal { get; set; }
        // order date 
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string PaymentIntentId { get; set; }



    }
}
