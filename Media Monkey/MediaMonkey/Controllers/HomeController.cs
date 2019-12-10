using System.Runtime.InteropServices.ComTypes;
using System.Xml.Schema;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MediaMonkey.Models;
using MediaMonkey.DataAccess.Models;
using System.Security.Claims;

namespace MediaMonkey.Controllers
{
    [Authorize, Route("")]
    public class HomeController : Controller
    {
        private readonly AppConfig _appConfig;

        private readonly DataService _dataService;

        public HomeController(IOptions<AppConfig> appConfig, DataContext dataContext){
            _appConfig = appConfig.Value;
            _dataService = new DataService(dataContext);
        }

        [AllowAnonymous, Route("")]
        public IActionResult Index()
        {
            List<VideoViewModel> model = new List<VideoViewModel>();
            List<VideoInfo> videoInfoList = _dataService.GetVideoInfos();

            foreach(var v in videoInfoList)
            {
                v.Video.Path = _appConfig.ContentPath + v.Video.Path;
                model.Add(new VideoViewModel { videoInfo = v, LikeRatio = _dataService.GetVideoTotalLikesDislikes(v.VideoId) == 0 ? 0 : _dataService.GCD(_dataService.GetVideoLikes(v.VideoId).Count(), _dataService.GetVideoTotalLikesDislikes(v.VideoId))});
            }

            return View(model);
        }

        [AllowAnonymous, Route("about")]
        public IActionResult About()
        {
            return View();
        }

        [AllowAnonymous, Route("login")]
        public IActionResult Login()
        {
            return View();
        }
        
        [AllowAnonymous, Route("registration")]
        public IActionResult Registration(){
            return View();
        }

        [AllowAnonymous, Route("profile/{id:int}")]
        public IActionResult Profile(int id)
        {
            Profile model = _dataService.GetProfile(id);
            model.VideoInfos.ForEach(vi => vi.Video = _dataService.GetVideo(vi.VideoId));
            model.VideoInfos.ForEach(vi => vi.Profile = _dataService.GetProfile(vi.ProfileId));
            model.VideoInfos.ForEach(vi => vi.Visibility = _dataService.GetVisibility(vi.VisibilityId));


            foreach(var vi in model.VideoInfos)
            {
                vi.Video.Path = _appConfig.ContentPath + vi.Video.Path;
            }

            return View(model);
        }

        [AllowAnonymous, Route("video/{id:int}")]
        public IActionResult Video(int id)
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Video video = _dataService.GetVideo(id);
            
            // Gets the video info and updates the view count
            VideoInfo videoInfo = _dataService.GetVideoInfo(video);
            videoInfo.Views++;
            _dataService.UpdateVideoInfo(videoInfo);
            
            // Sets the current path for the video using appsettings
            videoInfo.Video.Path = _appConfig.ContentPath + videoInfo.Video.Path;

            VideoViewModel model = new VideoViewModel(){
                videoInfo = videoInfo,
                Likes = _dataService.GetVideoLikes(video.Id).Count(),
                Dislikes = _dataService.GetVideoDislikes(video.Id).Count(),
                ProfileLiked = false,
                ProfileDisliked = false
            };

            if (User != null)
            {
                model.ProfileLiked = _dataService.GetProfileLikedVideo(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value), video.Id);
                model.ProfileDisliked = _dataService.GetProfileDislikedVideo(Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value), video.Id);
                if (model.ProfileLiked == model.ProfileDisliked)
                {
                    model.ProfileLiked = false;
                    model.ProfileDisliked = false;
                }
            }

            return View(model);
        }

        [AllowAnonymous, Route("error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
