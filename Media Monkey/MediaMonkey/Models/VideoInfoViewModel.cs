using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediaMonkey.DataAccess.Models;

namespace MediaMonkey.Models
{
    /// <summary>
    /// View Model used to store a list of Visibilities for the VideoInfo model.
    /// </summary>
    public class VideoInfoViewModel : VideoInfo
    {
        public List<Visibility> Visibilities { get; set; }
    }
}