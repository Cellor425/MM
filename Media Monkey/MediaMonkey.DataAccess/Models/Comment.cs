using System;
using System.ComponentModel.DataAnnotations;

namespace MediaMonkey.DataAccess.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public string CommentContent { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}