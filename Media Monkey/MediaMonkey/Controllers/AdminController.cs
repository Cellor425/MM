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
    [Authorize(Roles = "Admin,Owner"), Route("admin")]
    public class AdminController : Controller
    {
        private readonly AppConfig _appConfig;

        private readonly DataService _dataService;

        public AdminController(IOptions<AppConfig> appConfig, DataContext dataContext){
            _appConfig = appConfig.Value;
            _dataService = new DataService(dataContext);
        }

        [Route("videos")]
        public IActionResult Videos()
        {
            List<VideoInfo> videoInfo = _dataService.GetVideoInfosAdmin();
            videoInfo.ForEach(vi => vi.Video = _dataService.GetVideo(vi.VideoId));
            videoInfo.ForEach(vi => vi.Profile = _dataService.GetProfile(vi.ProfileId));
            videoInfo.ForEach(vi => vi.Visibility = _dataService.GetVisibility(vi.VisibilityId));

            List<VideoViewModel> model = new List<VideoViewModel>();
            foreach(var v in videoInfo)
            {
                model.Add(new VideoViewModel(){
                    videoInfo = v,
                    Likes = _dataService.GetVideoLikes(v.Video.Id).Count(),
                    Dislikes = _dataService.GetVideoDislikes(v.Video.Id).Count()
                });
            }

            return View(model);
        }

        [HttpGet, Route("editvideo/{id:int}")]
        public IActionResult EditVideoInfo(int id)
        {
            VideoInfoViewModel model = new VideoInfoViewModel();

            VideoInfo info = _dataService.GetVideoInfo(id);
            model.Id = info.Id;
            model.Title = info.Title;
            model.Description = info.Description;
            model.UploadDate = info.UploadDate;
            model.ThumbnailPath = info.ThumbnailPath;
            model.ProfileId = info.ProfileId;
            model.Profile = info.Profile;
            model.VisibilityId = info.VisibilityId;
            model.Visibility = info.Visibility;
            model.VideoId = info.VideoId;
            model.Video = info.Video;
            model.IsUploadFinished = info.IsUploadFinished;
            model.IsArchived = info.IsArchived;

            model.Visibilities = _dataService.GetVisibilities();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, Route("editvideo/{id:int}")]
        public IActionResult EditVideoInfo(VideoInfo videoInfo)
        {
            if (!ModelState.IsValid)
            {
                VideoInfoViewModel model = new VideoInfoViewModel();

                VideoInfo info = _dataService.GetVideoInfo(videoInfo.Id);
                model.Id = info.Id;
                model.Description = info.Description;
                model.UploadDate = info.UploadDate;
                model.ThumbnailPath = info.ThumbnailPath;
                model.ProfileId = info.ProfileId;
                model.Profile = info.Profile;
                model.VisibilityId = info.VisibilityId;
                model.Visibility = info.Visibility;
                model.VideoId = info.VideoId;
                model.Video = info.Video;
                model.IsUploadFinished = info.IsUploadFinished;
                model.IsArchived = info.IsArchived;

                model.Visibilities = _dataService.GetVisibilities();

                return View(model);
            }
            
            _dataService.UpdateVideoInfo(videoInfo);

            return RedirectToAction("Videos");
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

            return RedirectToAction("Videos");         
        }

        [Route("error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
