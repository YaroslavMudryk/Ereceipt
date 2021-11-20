using System.ComponentModel.DataAnnotations;

namespace Ereceipt.Domain.Models
{
    public class Receipt : BaseModel<long>
    {
        [Required]
        public bool IsImportant { get; set; }
        [Required]
        public bool CanEdit { get; set; }
        public double TotalPrice { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public int? ShopId { get; set; }
        public Shop Shop { get; set; }
        public int? LoyaltyCardId { get; set; }
        public LoyaltyCard LoyaltyCard { get; set; }
        public int? GroupId { get; set; }
        public Group Group { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        [Url]
        public string ReceiptLink { get; set; }
        public DateTime? ReceiptDate { get; set; }
        public List<ProductReceipt> ProductReceipts { get; set; }
        public List<Comment> Comments { get; set; }
    }
}