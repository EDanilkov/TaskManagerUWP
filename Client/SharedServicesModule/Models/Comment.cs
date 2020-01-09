using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedServicesModule.Models
{
    public partial class Comment
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TaskId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        public virtual Task Task { get; set; }

        public virtual User User { get; set; }

        public DateTime DateTime { get; set; }
        
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        
        [Required]
        [StringLength(250)]
        public string Text { get; set; }
    }
}
