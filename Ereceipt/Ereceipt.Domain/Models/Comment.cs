using System.ComponentModel.DataAnnotations;

namespace Ereceipt.Domain.Models
{
    public class Comment : BaseModel<long>
    {
        [Required, StringLength(1500, MinimumLength = 1)]
        public string Text { get; set; }
        public int? FromId { get; set; }
        //public User From { get; set; }
        public long ReceiptId { get; set; }
        public Receipt Receipt { get; set; }
    }
}