namespace TestTask.Models
{
    public class ProductAudit
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? OldTitle { get; set; }
        public int? OldQuantity { get; set; }
        public decimal? OldPrice { get; set; }
        public string? NewTitle { get; set; }
        public int? NewQuantity { get; set; }
        public decimal? NewPrice { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangeDate { get; set; }
        public ChangeType ChangeType { get; set; }

        public Product Product { get; set; }

    }

    public enum ChangeType
    {
        Create,
        Update,
        Delete
    }
}
