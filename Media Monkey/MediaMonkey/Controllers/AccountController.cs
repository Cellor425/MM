using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using MediaMonkey.Models;
using MediaMonkey.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace MediaMonkey.Controllers
{
    // Authorize tells the program that they have to be signin to access this controller.
    // This should be on all login pages and add [AllowAnonymous] to areas where login is not needed.
    [Authorize]
    [Route("")]
    public class AccountController : Controller  
    {
        private readonly DataService _dataService;

        public AccountController(DataContext dataContext)
        {
            // Instantiate an instance of the data service.
            _dataService = new DataService(dataContext);
        }

        [AllowAnonymous, HttpGet, Route("register")]
        public IActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel(){
                AllCountries = _dataService.GetCountries()
            };
            return View(model);
        }

        [AllowAnonymous, HttpPost, Route("register")]
        public IActionResult Register(RegisterViewModel registerViewModel, IFormFile upload)
        {
            if (!ModelState.IsValid)
            {
                RegisterViewModel model = new RegisterViewModel(){
                    AllCountries = _dataService.GetCountries()
                };
                return View(model);
            }

            // Check if the email exists
            Profile existingProfile = _dataService.GetProfile(registerViewModel.EmailAddress);
            if (existingProfile != null) 
            {
                // Set email address already in use error message.
                ModelState.AddModelError("Error", "An account already exists with that email address.");

                return View();
            }

            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

            // Create the user profile
            Profile user = new Profile()
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Username = registerViewModel.Username,
                Email = registerViewModel.EmailAddress,
                Password = passwordHasher.HashPassword(null, registerViewModel.Password),
                CreationDate = DateTime.Now,
                CountryId = registerViewModel.CountryId
            };
        
            // Upload avatar image
            if (upload != null && upload.Length > 0)
            {
                var avatar = new Avatar
                {
                    FileName = System.IO.Path.GetFileName(upload.FileName),
                    ContentType = upload.ContentType
                };

                using (var reader = new System.IO.BinaryReader(upload.OpenReadStream()))
                {
                    avatar.Content = reader.ReadBytes((int)upload.Length);
                }

                user.Avatar = avatar;
            }

            _dataService.AddProfile(user);

            return RedirectToAction("Login", "Account");
        }

        
        [AllowAnonymous, HttpGet, Route("sign-in")]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous, ValidateAntiForgeryToken, HttpPost, Route("sign-in")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {          
            if (!ModelState.IsValid)
            {
                return View();
            }

            Profile user = _dataService.GetProfile(loginViewModel.EmailAddress);

            // If user is not found.
            if (user == null) 
            {
                // Set email address not registered error message.
                ModelState.AddModelError("Error", "An account does not exist with that email address.");
            
                return View();
            }

            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
            PasswordVerificationResult passwordVerificationResult = 
                passwordHasher.VerifyHashedPassword(null, user.Password, loginViewModel.Password);
                
            // If password fails
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                // Set invalid password error message.
                ModelState.AddModelError("Error", "Invalid password.");

                return View();
            }
            
            if (passwordVerificationResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                
            }
            
            // A claim is part of a users id.(name value pair)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.Role, user.IsOwner ? "Owner" : user.IsAdmin ? "Admin" : "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties {};

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity), 
                authProperties);
            
            // Update the last login time
            user.LastLogin = DateTime.Now;
            _dataService.UpdateProfile(user);

            // Route to url user was on last
            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                return Redirect(returnUrl);
            }
        }

        [Route("sign-out")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet, Route("access-denied")]
        public IActionResult AccessDenied()
        {
            return View();
        }   
    }
}