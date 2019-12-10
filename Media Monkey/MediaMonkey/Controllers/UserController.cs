using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MediaMonkey.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net;
using System.Security.Claims;
using MediaMonkey.Models;
using Microsoft.AspNetCore.Authorization;

namespace MediaMonkey.Controllers
{
    [Authorize, Route("user")]
    public class UserController : Controller
    {
        private readonly AppConfig _appConfig;

        private readonly DataService _dataService;

        public UserController(IOptions<AppConfig> appConfig, DataContext dataContext){
            _appConfig = appConfig.Value;
            _dataService = new DataService(dataContext);
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
            model.IsAdmin = DbProfile.IsAdmin;
            model.IsOwner = DbProfile.IsOwner;
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
                model.IsAdmin = DbProfile.IsAdmin;
                model.IsOwner = DbProfile.IsOwner;
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

            return RedirectToAction("Profile", "Home", new { id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value });
        }

        [HttpGet, Route("addvideo")]
        public IActionResult AddVideo()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, Route("addvideo")]
        public async Task<IActionResult> AddVideo(IFormFile file)
        {
            // Check if a file is there
            if (file == null || file.Length == 0)  
                return Content("file not selected");

            // Check if the file is a video file
            if (!file.ContentType.Contains("video"))
                return Content("File needs to be a video format");

            // Check that the file is not too big (files less than 50MB permitted)
            if ((file.Length / 1000000) > 50)
                return Content("File is too big");

            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(_appConfig.FTPUrl + file.FileName);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential("mi3-wss11\\mjschnei", "*K7GeR!qzQ9u23");

            // Create database objects for the video and video info in case of user disconnect
            Video video = new Video()
            {
                Path = "/videos/" + file.FileName,
                Size = (int)file.Length,
                Format = file.ContentType
            };

            int videoId = _dataService.AddVideo(video);

            VideoInfo videoInfo = new VideoInfo()
            {
                Title = "Unfinished",
                Description = string.Empty,
                UploadDate = DateTime.Now,
                ThumbnailPath = string.Empty,
                ProfileId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                VisibilityId = 2,
                VideoId = videoId,
                IsUploadFinished = false
            };

            int videoInfoId = _dataService.AddVideoInfo(videoInfo);

            // Uploads to local directory for testing purposes
            #if DEBUG
            using (FileStream stream = new FileStream(_appConfig.LocalUrl + file.FileName, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            #endif

            // Upload the file to the ftp
            using (var requestStream = request.GetRequestStream())  
            {  
                await file.CopyToAsync(requestStream);  
            }

            // Get a response
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
            }

            // Redirect to add video info page to finish the upload process
            return RedirectToAction("AddVideoInfo", new { id = videoInfoId });
        }

        // Called after uploading a video only, mimicks the edit video info functions
        [HttpGet, Route("addvideoinfo/{id:int}")]
        public IActionResult AddVideoInfo(int id)
        {
            VideoInfo info = _dataService.GetVideoInfo(id);

            // View Model used for the add screen
            VideoInfoViewModel model = new VideoInfoViewModel()
            {
                VideoId = info.VideoId,
                ProfileId = info.ProfileId,
                UploadDate = info.UploadDate,
                VisibilityId = info.VisibilityId,
                Visibilities = _dataService.GetVisibilities(),
                Title = info.Title,
                Description = info.Description,
                ThumbnailPath = info.ThumbnailPath,
                IsUploadFinished = info.IsUploadFinished
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Route("addvideoinfo/{id:int}")]
        public IActionResult AddVideoInfo(VideoInfo videoInfo)
        {
            if (!ModelState.IsValid)
            {
                VideoInfo info = _dataService.GetVideoInfo(videoInfo.Id);

                // View Model used for the add screen
                VideoInfoViewModel model = new VideoInfoViewModel()
                {
                    VideoId = info.VideoId,
                    ProfileId = info.ProfileId,
                    UploadDate = info.UploadDate,
                    VisibilityId = info.VisibilityId,
                    Visibilities = _dataService.GetVisibilities(),
                    Title = info.Title,
                    Description = info.Description,
                    ThumbnailPath = info.ThumbnailPath,
                    IsUploadFinished = info.IsUploadFinished
                };

                return View(model);
            }

            // Set the upload to finished before updating
            videoInfo.IsUploadFinished = true;

            _dataService.UpdateVideoInfo(videoInfo);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet, Route("editvideo/{id:int}")]
        public IActionResult EditVideoInfo(int id)
        {
            VideoInfo info = _dataService.GetVideoInfo(id);

            VideoInfoViewModel model = new VideoInfoViewModel(){
                Id = info.Id,
                Description = info.Description,
                UploadDate = info.UploadDate,
                ThumbnailPath = info.ThumbnailPath,
                ProfileId = info.ProfileId,
                Profile = info.Profile,
                VisibilityId = info.VisibilityId,
                Visibility = info.Visibility,
                VideoId = info.VideoId,
                Video = info.Video,
                IsUploadFinished = info.IsUploadFinished,
                IsArchived = info.IsArchived,
                Visibilities = _dataService.GetVisibilities()
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Route("editvideo/{id:int}")]
        public IActionResult EditVideoInfo(VideoInfo videoInfo)
        {
            if (!ModelState.IsValid)
            {
                VideoInfo info = _dataService.GetVideoInfo(videoInfo.Id);

                VideoInfoViewModel model = new VideoInfoViewModel(){
                    Id = info.Id,
                    Description = info.Description,
                    UploadDate = info.UploadDate,
                    ThumbnailPath = info.ThumbnailPath,
                    ProfileId = info.ProfileId,
                    Profile = info.Profile,
                    VisibilityId = info.VisibilityId,
                    Visibility = info.Visibility,
                    VideoId = info.VideoId,
                    Video = info.Video,
                    IsUploadFinished = info.IsUploadFinished,
                    IsArchived = info.IsArchived,
                    Visibilities = _dataService.GetVisibilities()
                };

                return View(model);
            }

            // Finish upload
            videoInfo.IsUploadFinished = true;
            
            _dataService.UpdateVideoInfo(videoInfo);

            return RedirectToAction("Profile", "Home", new { id = videoInfo.ProfileId });
        }

        // Archive a video by passing in the videoinfo id
        [HttpGet, Route("delete-videoinfo/{id:int}")]
        public IActionResult DeleteVideoInfo(int id)
        {
            VideoInfo model = _dataService.GetVideoInfo(id);
            model.Video = _dataService.GetVideo(model.VideoId);
            model.Profile = _dataService.GetProfile(model.ProfileId);
            model.Visibility = _dataService.GetVisibility(model.VisibilityId);

            return View(model);         
        }

        [HttpPost, ValidateAntiForgeryToken, Route("delete-videoinfo/{id:int}")]
        public IActionResult DeleteVideoInfo(VideoInfo videoInfo)
        {
            _dataService.DeleteVideoInfo(videoInfo.Id);

            return RedirectToAction("Index", "Home");         
        }

        [Route("error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}