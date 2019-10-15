using System.ComponentModel.DataAnnotations.Schema;

namespace ServerAPI.Data.Models
{
    [Table("RolePermission")]
    public partial class RolePermission
    {
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public int PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
