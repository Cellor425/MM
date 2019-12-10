using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediaMonkey.DataAccess.Models
{
    public class Avatar
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        [Required]
        public int ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
    }
}