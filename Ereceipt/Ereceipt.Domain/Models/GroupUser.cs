using System.ComponentModel.DataAnnotations;

namespace Ereceipt.Domain.Models
{
    public class GroupUser : BaseModel<long>
    {
        [StringLength(150, MinimumLength = 1)]
        public string Title { get; set; }
        public bool IsCreator { get; set; }
        public bool CanEditInfoGroup { get; set; }
        public bool CanAddReceiptToGroup { get; set; }
        public bool CanRemoveReceiptFromGroup { get; set; }
        public bool CanJoinNewMember { get; set; }
        public bool CanKickMember { get; set; }
        public bool CanEditPermissions { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
