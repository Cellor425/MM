using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediaMonkey.DataAccess.Models;

namespace MediaMonkey.Models
{
    public class VideoViewModel
    {
        public VideoInfo videoInfo { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public bool ProfileLiked { get; set; }
        public bool ProfileDisliked { get; set; }
        public double LikeRatio { get; set; }
    }
}