using System.ComponentModel.DataAnnotations;

namespace Ereceipt.Application.ViewModels.Users
{
    public class LoginEmailCreateModel : DeviceCreateModel
    {
        [Required, MinLength(3), MaxLength(100)]
        public string Login { get; set; }
        [MinLength(8), MaxLength(30), RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")]
        public string Password { get; set; }
        public AppInfoModel App { get; set; }
    }
}