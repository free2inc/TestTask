namespace TestTask.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPriceWithVAT(decimal vatRate)
        {
            return (Quantity * Price) * (1 + vatRate);
        }
    }
}
