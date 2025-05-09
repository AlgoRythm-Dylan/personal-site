﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Lib.Models
{
    [Table("Photographers")]
    public class Photographer
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(64)]
        public string FirstName { get; set; }
        [MaxLength(128)]
        public string? LastName { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
