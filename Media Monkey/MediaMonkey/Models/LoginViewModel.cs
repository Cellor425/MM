using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using MediaMonkey.DataAccess.Models;

namespace MediaMonkey.Models
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password{ get; set; }
    }
    
}