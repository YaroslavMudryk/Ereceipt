namespace Ereceipt.Domain.Models
{
    public class ProductReceipt : BaseModel<long>
    {
        public double QuantityOrWeight { get; set; }
        public double Price { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public long ReceiptId { get; set; }
        public Receipt Receipt { get; set; }
    }
}