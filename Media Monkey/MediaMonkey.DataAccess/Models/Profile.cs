using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediaMonkey.DataAccess.Models
{
    public class Profile
    {
        public int Id { get; set; }
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }
        [StringLength(30)]
        [Required]
        public string Username { get; set; }
        public string Description { get; set; }
        [Required]
        public string Email { get; set; }
        [Required, Display(Name = "Is Email Verified")]
        public bool IsEmailVerified { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public DateTime LastLogin { get; set; }
     
        [Required]
        public string Password { get; set; }

        [Required, Display(Name = "Is User an Admin")]
        public bool IsAdmin { get; set; }

        [Required, Display(Name = "Is User an Owner")]
        public bool IsOwner { get; set; }

        [Required, Display(Name = "Is User Archived")]
        public bool IsArchived { get; set; } 

        [Required, Display(Name = "Country of Origin")]
        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }
        public List<VideoInfo> VideoInfos { get; set; }
        public List<ProfileLikedVideo> LikedVideos { get; set; }
        public List<ProfileDislikedVideo> DislikedVideos { get; set; }
        public virtual Avatar Avatar { get; set; }
    }
}