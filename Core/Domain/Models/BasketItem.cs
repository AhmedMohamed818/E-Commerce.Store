namespace Domain.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
       
        //public string Brand { get; set; }
        //public string Type { get; set; }
        //public string Color { get; set; }
        //public string Size { get; set; }
        //public string Material { get; set; }
        //public string Description { get; set; }
        //public string Author { get; set; }
        //public string Publisher { get; set; }

    }
}