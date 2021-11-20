using System.ComponentModel.DataAnnotations;
namespace Ereceipt.Domain.Models
{
    public class User : BaseModel<int>
    {
        [Required, MinLength(2), MaxLength(250)]
        public string Name { get; set; }
        [Required, MinLength(4), MaxLength(250)]
        public string Username { get; set; }
        [MinLength(10), MaxLength(1000)]
        public string Avatar { get; set; }
        [MinLength(1), MaxLength(80)]
        public string About { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<Session> Sessions { get; set; }
        public List<UserLogin> UserLogins { get; set; }
        public List<Receipt> Receipts { get; set; }
        public List<Comment> Comments { get; set; }
        public List<GroupUser> GroupUsers { get; set; }
    }
}