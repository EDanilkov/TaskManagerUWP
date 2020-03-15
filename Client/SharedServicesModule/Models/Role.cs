using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedServicesModule.Models
{
    [Table("Role")]
    public class Role
    {
        public Role()
        {
            UserProject = new HashSet<UserProject>();
            Permission = new HashSet<Permission>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        public virtual ICollection<UserProject> UserProject { get; set; }
        
        public virtual ICollection<Permission> Permission { get; set; }
    }
}
