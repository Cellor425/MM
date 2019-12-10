using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediaMonkey.DataAccess.Models
{
    public class Video
    {
        public int Id { get; set; }
        [Required]
        public string Path { get; set; }
        public int Size { get; set; }
        [Required]
        public string Format { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public List<VideoInfo> VideoInfos { get; set; }
    }
}