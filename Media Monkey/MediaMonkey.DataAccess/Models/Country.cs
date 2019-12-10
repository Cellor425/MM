using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediaMonkey.DataAccess.Models
{
    public class Country
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string CountryName { get; set; }
        public List<Profile> Profiles { get; set; }
    }
}