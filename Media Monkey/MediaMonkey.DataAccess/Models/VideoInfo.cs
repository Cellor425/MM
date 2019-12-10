using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaMonkey.DataAccess.Models
{
    public class VideoInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime UploadDate { get; set; }
        [Required]
        public int Views { get; set; }
        public string ThumbnailPath { get; set; }
        public int ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
        [Required]
        public int? VisibilityId { get; set; }
        public virtual Visibility Visibility { get; set; }
        public int VideoId { get; set; }
        public virtual Video Video { get; set; }
        public bool IsUploadFinished { get; set; }
        public bool IsArchived { get; set; }
    }
}