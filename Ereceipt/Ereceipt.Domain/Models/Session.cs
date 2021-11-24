using Extensions.DeviceDetector.Models;
using System.ComponentModel.DataAnnotations;
namespace Ereceipt.Domain.Models
{
    public class Session : BaseModel<Guid>
    {
        [Required]
        public bool IsActive { get; set; }
        public DateTime? DateUnActive { get; set; }
        public ClientInfo UnActiveFromDevice { get; set; }
        public App App { get; set; } // json object
        public ClientInfo Device { get; set; } // json object
        public Location Location { get; set; } //json object
        [StringLength(3500, MinimumLength = 5)]
        public string Token { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}