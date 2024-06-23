namespace kontorExpert.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Color { get; set; }
        public string Dimensions { get; set; }
        public int CategoryID { get; set; }
        public bool IsUsed { get; set; }
    }
}
