namespace Domain.Models.OrderModels
{
    public class OrderItem : BaseEntity<Guid>
    {
        public OrderItem()
        {
                
        }
        public OrderItem(ProductInOrederItem product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }

        public ProductInOrederItem Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }


    }
}