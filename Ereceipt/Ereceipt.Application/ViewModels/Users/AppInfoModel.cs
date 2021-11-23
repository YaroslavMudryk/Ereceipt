using System.ComponentModel.DataAnnotations;

namespace Ereceipt.Application.ViewModels.Users
{
    public class AppInfoModel
    {
        [Required]
        public string AppId { get; set; }
        [Required]
        public string AppSecret { get; set; }
    }
}