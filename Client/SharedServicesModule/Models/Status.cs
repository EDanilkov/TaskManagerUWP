﻿using System.ComponentModel.DataAnnotations;

namespace SharedServicesModule.Models
{
    public partial class Status
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
