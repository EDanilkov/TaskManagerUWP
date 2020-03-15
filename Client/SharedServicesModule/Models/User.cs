using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedServicesModule.Models
{
    [Table("User")]
    public class User
    {
        public User()
        {
            UserProject = new HashSet<UserProject>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public DateTime? RegistrationDate { get; set; }
        
        public virtual ICollection<UserProject> UserProject { get; set; }
    }
}
