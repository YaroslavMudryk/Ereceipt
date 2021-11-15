using System.ComponentModel.DataAnnotations;

namespace Ereceipt.Domain.Models
{
    public class Shop : BaseModel<int>
    {
        [Required, StringLength(500, MinimumLength = 2)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountOfEmployees { get; set; } = 0;
        public string Image { get; set; }
        [Url]
        public string WebSite { get; set; }
    }
}
