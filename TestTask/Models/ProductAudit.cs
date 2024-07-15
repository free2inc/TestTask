namespace TestTask.Models
{
    public class ProductAudit
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ChangeType { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangeDate { get; set; }

    }
}
