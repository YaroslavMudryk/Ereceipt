namespace Ereceipt.Application.ViewModels.Authentication
{
    public class ConfirmEmailCreateModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
