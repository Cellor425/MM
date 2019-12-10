using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediaMonkey.DataAccess.Models
{
    public class Visibility
    {
        public int Id { get; set; }
        [Required]
        [StringLength(25)]
        public string VisibilityName { get; set; }
        public List<VideoInfo> VideoInfos { get; set; }
    }
}