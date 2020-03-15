using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedServicesModule.Models
{
    [Table("Project")]
    public class Project
    {
        public Project()
        {
            Task = new HashSet<Task>();
            UserProject = new HashSet<UserProject>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public int AdminId { get; set; }
        
        public virtual ICollection<Task> Task { get; set; }
        
        public virtual ICollection<UserProject> UserProject { get; set; }
    }
}
