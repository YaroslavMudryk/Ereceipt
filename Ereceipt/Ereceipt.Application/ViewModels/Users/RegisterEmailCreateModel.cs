using System.ComponentModel.DataAnnotations;

namespace Ereceipt.Application.ViewModels.Users
{
    public class RegisterEmailCreateModel : LoginEmailCreateModel
    {
        [Required, MinLength(1), MaxLength(250)]
        public string Name { get; set; }
        [StringLength(30, MinimumLength = 4)]
        public string Username { get; set; }
        [MinLength(1), MaxLength(80)]
        public string About { get; set; }
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}