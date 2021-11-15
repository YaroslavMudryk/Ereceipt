using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ereceipt.Domain.Models
{
    public class BaseCreateModel
    {
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedFromIP { get; set; }
    }
    public class BaseEditModel : BaseCreateModel
    {
        public DateTime? LastUpdatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
        public string LastUpdatedFromIP { get; set; }
    }
    public class BaseModel : BaseEditModel
    {
        public int Version { get; set; }
    }
    public class BaseModel<TypeId> : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TypeId Id { get; set; }
    }
}