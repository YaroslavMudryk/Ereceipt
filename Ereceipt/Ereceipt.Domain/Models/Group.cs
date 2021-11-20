using System.ComponentModel.DataAnnotations;

namespace Ereceipt.Domain.Models
{
    public class Group : BaseModel<int>
    {
        [Required, StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        [StringLength(1000, MinimumLength = 5)]
        public string Description { get; set; }
        public List<Receipt> Receipts { get; set; }
        public List<GroupUser> GroupUsers { get; set; }
    }
}
