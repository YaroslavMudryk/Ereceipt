using System.ComponentModel.DataAnnotations;
namespace Ereceipt.Domain.Models
{
    public class LoyaltyCard : BaseModel<int>
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Barcode { get; set; }
        public string Image { get; set; }
        public List<Receipt> Receipts { get; set; }
    }
}