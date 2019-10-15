using System.ComponentModel.DataAnnotations.Schema;

namespace ServerAPI.Data.Models
{
    [Table("UserProject")]
    public partial class UserProject
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
