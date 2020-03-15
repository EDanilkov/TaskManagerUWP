using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedServicesModule.Models
{
    [Table("Permission")]
    public class Permission
    {
        public Permission()
        {
            Role = new HashSet<Role>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        public virtual ICollection<Role> Role { get; set; }
    }
}
