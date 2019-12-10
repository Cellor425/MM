using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediaMonkey.DataAccess.Models;

namespace MediaMonkey.Models
{
    public class ProfilePageViewModel
    {
        public Profile User { get; set; }
        public List<Video> UsersVideos { get; set; }
    }
}