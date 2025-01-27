using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using MediaMonkey.DataAccess.Models;

namespace MediaMonkey.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Password must be between 5 and 255 characters", MinimumLength = 5)]

        public string Password { get; set;}

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfirm { get; set;}
        public List<Country> AllCountries { get; set; }
        public int CountryId { get; set; }
        public virtual Avatar Avatar { get; set; }
    }
    
}