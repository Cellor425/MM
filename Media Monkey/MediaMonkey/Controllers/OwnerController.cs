using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MediaMonkey.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net;
using System.Security.Claims;
using MediaMonkey.Models;

namespace MediaMonkey.Controllers
{
    [Authorize(Roles = "Owner"), Route("admin")]
    public class OwnerController : Controller
    {
        private readonly AppConfig _appConfig;

        private readonly DataService _dataService;

        public OwnerController(IOptions<AppConfig> appConfig, DataContext dataContext){
            _appConfig = appConfig.Value;
            _dataService = new DataService(dataContext);
        }

        [Route("profiles")]
        public IActionResult Profiles()
        {
            List<Profile> model = _dataService.GetProfiles();

            return View(model);
        }

        [HttpGet, Route("editprofile/{id:int}")]
        public IActionResult EditProfile(int id)
        {
            ProfileViewModel model = new ProfileViewModel();
            Profile DbProfile = _dataService.GetProfile(id);
            model.Countries = _dataService.GetCountries();
            model.Username = DbProfile.Username;
            model.Email = DbProfile.Email;
            model.CreationDate = DbProfile.CreationDate;
            model.LastLogin = DbProfile.LastLogin;
            model.Password = DbProfile.Password;
            model.CountryId = DbProfile.CountryId;
            model.Country = DbProfile.Country;
            model.IsOwner = DbProfile.IsOwner;
            model.IsAdmin = DbProfile.IsAdmin;
            model.IsEmailVerified = DbProfile.IsEmailVerified;
            model.IsArchived = DbProfile.IsArchived;
            model.FirstName = DbProfile.FirstName;
            model.LastName = DbProfile.LastName;
            model.Description = DbProfile.Description;
            model.Avatar = DbProfile.Avatar;
            
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Route("editprofile/{id:int}")]
        public IActionResult EditProfile(Profile profile, IFormFile upload)
        {
            if (!ModelState.IsValid)
            {
                ProfileViewModel model = new ProfileViewModel();
                Profile DbProfile = _dataService.GetProfile(profile.Id);
                model.Countries = _dataService.GetCountries();
                model.Username = DbProfile.Username;
                model.Email = DbProfile.Email;
                model.CreationDate = DbProfile.CreationDate;
                model.LastLogin = DbProfile.LastLogin;
                model.Password = DbProfile.Password;
                model.CountryId = DbProfile.CountryId;
                model.Country = DbProfile.Country;
                model.IsOwner = DbProfile.IsOwner;
                model.IsAdmin = DbProfile.IsAdmin;
                model.IsArchived = DbProfile.IsArchived;
                model.IsEmailVerified = DbProfile.IsEmailVerified;
                model.FirstName = DbProfile.FirstName;
                model.LastName = DbProfile.LastName;
                model.Description = DbProfile.Description;
                model.Avatar = DbProfile.Avatar;
            
                return View(model);
            }

            // Upload avatar image
            if (upload != null && upload.Length > 0)
            {
                // Note: Add functionality to delete the current avatar image if it exists

                var avatar = new Avatar
                {
                    FileName = System.IO.Path.GetFileName(upload.FileName),
                    ContentType = upload.ContentType
                };

                using (var reader = new System.IO.BinaryReader(upload.OpenReadStream()))
                {
                    avatar.Content = reader.ReadBytes((int)upload.Length);
                }

                profile.Avatar = avatar;
            }
            
            // Note: Add function to change verified status to false if the email is changed.
            _dataService.UpdateProfile(profile);

            return RedirectToAction("Profiles");
        }

        [Route("error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
