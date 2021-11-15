using System.ComponentModel.DataAnnotations;
namespace Ereceipt.Domain.Models
{
    public class UserLogin : BaseModel<int>
    {
        [Required, MinLength(3), MaxLength(100)]
        public string Login { get; set; }
        [MinLength(5), MaxLength(2000)]
        public string Password { get; set; }
        [Required]
        public bool IsConfirm { get; set; }
        public DateTime? ConfirmAt { get; set; }
        [Required, StringLength(300, MinimumLength = 50)]
        public string TokenConfirm { get; set; }
        public string Type { get; set; } // email, facebook, twitter, google, microsoft
        public int UserId { get; set; }
        public User User { get; set; }
    }
}