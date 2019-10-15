using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerAPI.Data.Models
{
    [Table("Project")]
    public partial class Project
    {
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
    }
}
