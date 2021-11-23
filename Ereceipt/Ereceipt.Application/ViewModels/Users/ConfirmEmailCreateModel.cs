using System.ComponentModel.DataAnnotations;

namespace Ereceipt.Application.ViewModels.Users
{
    public class ConfirmEmailCreateModel : DeviceCreateModel
    {
        [Required, EmailAddress]
        public string EmailAddress { get; set; }
        [Required, StringLength(50)]
        public string Token { get; set; }
    }
}