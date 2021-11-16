using System.ComponentModel.DataAnnotations;

namespace Ereceipt.Domain.Models
{
    public class Product : BaseModel<long>
    {
        [Required, StringLength(150, MinimumLength = 2)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Producer { get; set; }
        public string Image { get; set; }
        public string[] Barcodes { get; set; } // json object
    }
}