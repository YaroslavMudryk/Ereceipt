using System.ComponentModel.DataAnnotations;

namespace Ereceipt.Domain.Models
{
    public class Role : BaseModel<int>
    {
        [Required]
        public string Name { get; set; }
        public string SystemName { get; set; }
        [Required]
        public int Lvl { get; set; }
        public List<UserRole> UserRoles { get; set;}
    }
}