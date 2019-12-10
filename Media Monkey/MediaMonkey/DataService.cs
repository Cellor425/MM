using System;
using System.Collections.Generic;
using System.Linq;
using MediaMonkey.Models;
using MediaMonkey.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace MediaMonkey
{
    public class DataService
    {
        private DataContext _dataContext;

        public DataService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<Video> GetVideos()
        {
            return _dataContext.Videos.ToList();
        }

        public VideoInfo GetVideoInfo(int id)
        {
            return _dataContext.VideoInfos
                .Include(x => x.Video)
                .ThenInclude(v => v.Comments)
                .ThenInclude(c => c.Profile)
                .Include(x => x.Profile)
                .First(x => x.Id == id);
        }

        // Overloaded method for video page
        public VideoInfo GetVideoInfo(Video video)
        {
            return _dataContext.VideoInfos
                .Include(x => x.Video)
                .ThenInclude(v => v.Comments)
                .ThenInclude(c => c.Profile)
                .Include(x => x.Profile)
                .First(x => x.VideoId == video.Id);
        }

        // Return only unarchieved videos
        public List<VideoInfo> GetVideoInfos()
        {
            return _dataContext.VideoInfos
                .Where(x => !x.IsArchived)
                .Include(x => x.Video)
                .ThenInclude(v => v.Comments)
                .ThenInclude(c => c.Profile)
                .Include(x => x.Profile)
                .Include(x => x.Visibility)
                .ToList();
        }

        // Return all videos
        public List<VideoInfo> GetVideoInfosAdmin()
        {
            return _dataContext.VideoInfos
                .Include(x => x.Video)
                .ThenInclude(v => v.Comments)
                .ThenInclude(c => c.Profile)
                .Include(x => x.Profile)
                .ToList();
        }

        public int GCD(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                 if (a > b)
                    a %= b;
                 else
                    b %= a;
            }
            
            if (a == 0 && b == 0)
                return 0;
            else if (a == 0)
                 return -b;
            else
                 return a;
        }

        public List<VideoInfo> GetVideoInfosByProfile(int id)
        {
            return _dataContext.VideoInfos.Where(x => x.ProfileId == id).ToList();
        }

        public List<ProfileLikedVideo> GetProfileLikedVideos(int id)
        {
            return _dataContext.ProfileLikedVideos.Where(x => x.ProfileId == id).ToList();
        }

        public List<ProfileDislikedVideo> GetProfileDislikedVideos(int id)
        {
            return _dataContext.ProfileDislikedVideos.Where(x => x.ProfileId == id).ToList();
        }

        public List<ProfileLikedVideo> GetVideoLikes(int id)
        {
            return _dataContext.ProfileLikedVideos.Where(x => x.VideoId == id).ToList();
        }

        public List<ProfileDislikedVideo> GetVideoDislikes(int id)
        {
            return _dataContext.ProfileDislikedVideos.Where(x => x.VideoId == id).ToList();
        }

        public int GetVideoTotalLikesDislikes(int id)
        {
            return _dataContext.ProfileLikedVideos.Where(x => x.VideoId == id).ToList().Count() + _dataContext.ProfileDislikedVideos.Where(x => x.VideoId == id).ToList().Count();
        }

        public Video GetVideo(int id)
        {
            return _dataContext.Videos.Include(x => x.Comments).ThenInclude(c => c.Profile).ToList().Find(x => x.Id == id);
        }

        public bool GetProfileLikedVideo(int profileId, int videoId)
        {
            return _dataContext.ProfileLikedVideos.Where(x => x.VideoId == videoId && x.ProfileId == profileId).Count() > 0;
        }

        public bool GetProfileDislikedVideo(int profileId, int videoId)
        {
            return _dataContext.ProfileDislikedVideos.Where(x => x.VideoId == videoId && x.ProfileId == profileId).Count() > 0;
        }

        public int AddVideo(Video video)
        {
            // Checks if any records exist
            // Adds the video if it does not exist, gets the video object if it does (Possible refactor)
            if (!_dataContext.Videos.Where(x => x.Path == video.Path && 
                                           x.Format == video.Format && 
                                           x.Size == video.Size).Any())
            {
                _dataContext.Videos.Add(video);
                _dataContext.SaveChanges();
            }
            else
            {
                video = _dataContext.Videos.First(x => x.Path == video.Path && 
                                                  x.Format == video.Format && 
                                                  x.Size == video.Size);
            }

            // Returns the id that was added
            return video.Id;
        }

        public int AddVideoInfo(VideoInfo videoInfo)
        {
            // Checks if any records exist
            // Adds the video if it does not exist, updates the video if it does
            if (!_dataContext.VideoInfos.Where(x => x.VideoId == videoInfo.VideoId && 
                                                    x.ProfileId == videoInfo.ProfileId &&
                                                    x.Title == videoInfo.Title).Any())
            {
                _dataContext.VideoInfos.Add(videoInfo);
                _dataContext.SaveChanges();
            }
            else
            {
                videoInfo = _dataContext.VideoInfos.First(x => x.VideoId == videoInfo.VideoId && 
                                                          x.ProfileId == videoInfo.ProfileId &&
                                                          x.Title == videoInfo.Title);
                UpdateVideoInfo(videoInfo);
            }
            
            // Returns the id that was added
            return videoInfo.Id;
        }

        public Profile GetProfile(int id)
        {
            return _dataContext.Profiles
                    .Include(p => p.Country)
                    .Include(p => p.VideoInfos)
                    .Include(p => p.Avatar)
                    .First(x => x.Id == id);
        }

        public List<Country> GetCountries()
        {
            return _dataContext.Countries.ToList();
        }

        public List<Profile> GetProfiles()
        {
            return _dataContext.Profiles
                .Include(p => p.Country)
                .Include(p => p.VideoInfos)
                .Include(p => p.Avatar)
                .ToList();
        }

        public void AddProfile(Profile profile)
        {
            _dataContext.Profiles.Add(profile);
            _dataContext.SaveChanges();
        }

        public Visibility GetVisibility(int? id)
        {
            return _dataContext.Visibilities.FirstOrDefault(v => v.Id == id);
        }

        public Visibility GetVisibility(Visibility visibility)
        {
            return _dataContext.Visibilities.FirstOrDefault(v => v.Id == visibility.Id);
        }

        public List<Visibility> GetVisibilities()
        {
            return _dataContext.Visibilities.ToList();
        }

        public void UpdateVideoInfo(VideoInfo videoInfo)
        {
            _dataContext.VideoInfos.Update(videoInfo).Property(x=>x.Id).IsModified = false;
            _dataContext.SaveChanges();
        }

        public void UpdateProfile(Profile profile)
        {
            // Checks if there is an avatar to update
            if (_dataContext.Avatars.Any(x => x.ProfileId == profile.Id))
            {
                UpdateAvatar(profile.Avatar, profile);
            }

            _dataContext.Profiles.Update(profile);
            _dataContext.SaveChanges();
        }

        public void UpdateAvatar(Avatar avatar, Profile profile)
        {
            _dataContext.Avatars.Remove(_dataContext.Avatars.First(x => x.ProfileId == profile.Id));
            _dataContext.SaveChanges();
        }

        public Profile GetProfile(string emailAddress)
        {
            return _dataContext.Profiles.ToList()
            .Find(x=>x.Email == emailAddress && x.IsArchived == false);
        }

        public void DeleteVideoInfo(int id)
        {
            // Upload archieved bool to delete the videoinfo
            VideoInfo videoInfo = GetVideoInfo(id);
            videoInfo.IsArchived = true;
            UpdateVideoInfo(videoInfo);
        }

        public Avatar GetAvatar(int id)
        {
            return _dataContext.Avatars.First(x => x.Id == id);
        }
    }
}