using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediaMonkey.DataAccess.Models;

namespace MediaMonkey.Models
{
    /// <summary>
    /// View model used to store a list of countries used for the profile model.
    /// </summary>
    public class ProfileViewModel : Profile
    {
        public List<Country> Countries { get; set; }
    }
}