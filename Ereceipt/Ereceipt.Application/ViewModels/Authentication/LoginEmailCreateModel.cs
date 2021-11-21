using System.ComponentModel.DataAnnotations;

namespace Ereceipt.Application.ViewModels.Authentication
{
    public class LoginEmailCreateModel : RequestModel
    {
        [Required, MinLength(3), MaxLength(100)]
        public string Login { get; set; }
        [MinLength(8), MaxLength(50)]
        public string Password { get; set; }
        [Required]
        public string AppId { get; set; }
        [Required]
        public string AppSecret { get; set; }
        public string AppVersion { get; set; }
        public UserInfoViewModel UserInfo { get; set; }
        [Required]
        public string Device { get; set; }
        [Required]
        public string Platform { get; set; }
        [Required]
        public string DeviceType { get; set; }
    }
}
