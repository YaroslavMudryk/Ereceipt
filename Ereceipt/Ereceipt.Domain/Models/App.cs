using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ereceipt.Domain.Models
{
    public class App : BaseModel<int>
    {
        [Required, StringLength(250, MinimumLength = 5)]
        public string Name { get; set; }
        [StringLength(50, MinimumLength = 1)]
        public string ShortName { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsOfficial { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTill { get; set; }
        [NotMapped]
        public bool IsActiveByDate => ActiveFrom < DateTime.Now && ActiveTill > DateTime.Now;
        [Required]
        public bool InDevelopment { get; set; }
        [Required]
        public string AppId { get; set; }
        [Required]
        public string AppSecret { get; set; }
        [StringLength(250, MinimumLength = 5), Url]
        public string CompanyUrl { get; set; }
    }
}