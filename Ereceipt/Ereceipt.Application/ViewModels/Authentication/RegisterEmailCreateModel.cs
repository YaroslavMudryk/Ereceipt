namespace Ereceipt.Application.ViewModels.Authentication
{
    public class RegisterEmailCreateModel
    {
        [Required, MinLength(3), MaxLength(100)]
        public string Login { get; set; }
        [MinLength(8), MaxLength(50)]
        public string Password { get; set; }
        [Required, StringLength(150)]
        public string FirstName { get; set; }
        [StringLength(150)]
        public string LastName { get; set; }
        public UserInfoViewModel UserInfo { get; set; }
        [Required]
        public RegisterInfo AdditionalInfo { get; set; }
    }
}
