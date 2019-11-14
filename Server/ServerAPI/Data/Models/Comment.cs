using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServerAPI.Data.Models
{
    [Table("Comment")]
    public partial class Comment
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(250)]
        public string Text { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
    }
}
