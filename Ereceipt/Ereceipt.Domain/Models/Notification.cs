using System.ComponentModel.DataAnnotations;
namespace Ereceipt.Domain.Models
{
    public class Notification : BaseModel<long>
    {
        [Required, StringLength(150, MinimumLength = 5)]
        public string Title { get; set; }
        [Required, StringLength(3000, MinimumLength = 10)]
        public string Description { get; set; }
        [StringLength(300, MinimumLength = 5)]
        public string Icon { get; set; }
        [Required]
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public string Payload { get; set; }
        public bool IsImportant { get; set; }
        public NotificationType Type { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}