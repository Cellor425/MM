using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaMonkey.DataAccess.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationship for profile and country
            modelBuilder.Entity<Profile>()
                .HasOne(p => p.Country)
                .WithMany(c => c.Profiles);

            // Define composite key for VideoInfo.
            modelBuilder.Entity<VideoInfo>()
                .HasKey(vi => new { vi.ProfileId, vi.VideoId });

            // Define relationship between VideoInfo and Video
            modelBuilder.Entity<VideoInfo>()
                .HasOne(vi => vi.Video)
                .WithMany(v => v.VideoInfos)
                .HasForeignKey(vi => vi.VideoId);

            // Define relationship between VideoInfo and Profile
            modelBuilder.Entity<VideoInfo>()
                .HasOne(vi => vi.Profile)
                .WithMany(p => p.VideoInfos)
                .HasForeignKey(vi => vi.ProfileId);

            // Define relationship between VideoInfo and Visibility            
            modelBuilder.Entity<VideoInfo>()
                .HasOne(vi => vi.Visibility)
                .WithMany(vis => vis.VideoInfos)
                .IsRequired();

            // Define relationship between UserDislikedVideo and Video
            modelBuilder.Entity<ProfileDislikedVideo>()
                .HasOne(udv => udv.Profile)
                .WithMany(p => p.DislikedVideos)
                .HasForeignKey(udv => udv.ProfileId);

            // Define relationship between UserLikedVideo and Video
            modelBuilder.Entity<ProfileLikedVideo>()
                .HasOne(ulv => ulv.Profile)
                .WithMany(p => p.LikedVideos)
                .HasForeignKey(ulv => ulv.ProfileId);

            // Define relationship between Avatar and Profile
            modelBuilder.Entity<Avatar>()
                .HasOne(a => a.Profile)
                .WithOne(p => p.Avatar);

            // Seed Data
            // Countries
            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, CountryName = "US"},
                new Country { Id = 2, CountryName = "Canda"}
            );

            // Visibilities
            modelBuilder.Entity<Visibility>().HasData(
                new Visibility { Id = 1, VisibilityName = "Public"},
                new Visibility { Id = 2, VisibilityName = "Private"}
            );

            // Profiles
            modelBuilder.Entity<Profile>().HasData(
                new Profile { Id = 1, FirstName = "John", LastName = "Doe", Description = "Joe is the master.", Username = "jDoe37", Email = "jdoe@gmail.com", IsEmailVerified = true, CreationDate = DateTime.Parse("2019-05-01 13:14:04.0000000"), LastLogin = DateTime.Now, Password = "AQAAAAEAACcQAAAAEKQ1ccM69zyRO+RbwtB3muSaviqjcj+kXa9Ee7Ou3BWhg7YxKkVr8PF8YttEv/ypeQ==", IsAdmin = true, IsOwner = true, IsArchived = false, CountryId = 1}
            );

            // Videos
            modelBuilder.Entity<Video>().HasData(
                new Video { Id = 1, Path = "/videos/Color.mp4", Size = 8883922, Format = "video/mp4"},
                new Video { Id = 2, Path = "/videos/Wausau.mp4", Size = 7712840, Format = "video/mp4"},
                new Video { Id = 3, Path = "/videos/Test3.mp4", Size = 5253880, Format = "video/mp4"},
                new Video { Id = 4, Path = "/videos/Gone Fishing.mp4", Size = 9168849, Format = "video/mp4"},
                new Video { Id = 5, Path = "/videos/Higher - Phantom 3 Standard.mp4", Size = 23374987, Format = "video/mp4"},
                new Video { Id = 6, Path = "/videos/NTCpie1.MOV", Size = 0, Format = "video/mp4"},
                new Video { Id = 7, Path = "/videos/NTCpie2.MOV", Size = 0, Format = "video/mp4"},
                new Video { Id = 8, Path = "/videos/NTCpie3.MOV", Size = 0, Format = "video/mp4"},
                new Video { Id = 9, Path = "/videos/NTCpie4.MOV", Size = 0, Format = "video/mp4"},
                new Video { Id = 10, Path = "/videos/NTCpie5.MOV", Size = 0, Format = "video/mp4"}
            );

            // VideoInfos
            modelBuilder.Entity<VideoInfo>().HasData(
                new VideoInfo { Id = 1, Title = "Colors", Description = "A colorful video reel with some music.", UploadDate = DateTime.Parse("2019-05-01 13:19:45.0000000"), Views = 0, ThumbnailPath = null, ProfileId = 1, VisibilityId = 1, VideoId = 1, IsUploadFinished = true, IsArchived = false},
                new VideoInfo { Id = 2, Title = "Unfinished", Description = "A video showing some area around Wausau, WI.", UploadDate = DateTime.Parse("2019-05-01 13:21:41.0000000"), Views = 0, ThumbnailPath = null, ProfileId = 1, VisibilityId = 2, VideoId = 2, IsUploadFinished = true, IsArchived = false},
                new VideoInfo { Id = 3, Title = "Unfinished", Description = "", UploadDate = DateTime.Parse("2019-05-01 13:22:48.0833931"), Views = 0, ThumbnailPath = null, ProfileId = 1, VisibilityId = 2, VideoId = 3, IsUploadFinished = false, IsArchived = false},
                new VideoInfo { Id = 4, Title = "Gone Fishing", Description = "A video of some fishing spots.", UploadDate = DateTime.Parse("2019-05-01 13:23:03.0000000"), Views = 0, ThumbnailPath = null, ProfileId = 1, VisibilityId = 1, VideoId = 4, IsUploadFinished = true, IsArchived = false},
                new VideoInfo { Id = 5, Title = "Higher- Phantom 3 Standard", Description = "What?", UploadDate = DateTime.Parse("2019-05-01 13:23:42.0000000"), Views = 0, ThumbnailPath = null, ProfileId = 1, VisibilityId = 1, VideoId = 5, IsUploadFinished = true, IsArchived = false},
                new VideoInfo { Id = 6, Title = "NTCpie1", Description = "Pie to the Face", UploadDate = DateTime.Parse("2019-05-01 13:23:42.0000000"), Views = 0, ThumbnailPath = null, ProfileId = 1, VisibilityId = 1, VideoId = 6, IsUploadFinished = true, IsArchived = false},
                new VideoInfo { Id = 7, Title = "NTCpie2", Description = "Pie to the Face", UploadDate = DateTime.Parse("2019-05-01 13:23:42.0000000"), Views = 0, ThumbnailPath = null, ProfileId = 1, VisibilityId = 1, VideoId = 7, IsUploadFinished = true, IsArchived = false},
                new VideoInfo { Id = 8, Title = "NTCpie3", Description = "Pie to the Face", UploadDate = DateTime.Parse("2019-05-01 13:23:42.0000000"), Views = 0, ThumbnailPath = null, ProfileId = 1, VisibilityId = 1, VideoId = 8, IsUploadFinished = true, IsArchived = false},
                new VideoInfo { Id = 9, Title = "NTCpie4", Description = "Pie to the Face", UploadDate = DateTime.Parse("2019-05-01 13:23:42.0000000"), Views = 0, ThumbnailPath = null, ProfileId = 1, VisibilityId = 1, VideoId = 9, IsUploadFinished = true, IsArchived = false},
                new VideoInfo { Id = 10, Title = "NTCpie5", Description = "Pie to the Face", UploadDate = DateTime.Parse("2019-05-01 13:23:42.0000000"), Views = 0, ThumbnailPath = null, ProfileId = 1, VisibilityId = 1, VideoId = 10, IsUploadFinished = true, IsArchived = false}

            );

            // ProfileLikedVideo
            modelBuilder.Entity<ProfileLikedVideo>().HasData(
                new ProfileLikedVideo { Id = 1, ProfileId = 1, VideoId = 1 },
                new ProfileLikedVideo { Id = 2, ProfileId = 1, VideoId = 4 }
            );

            // ProfileDislikedVideo
            modelBuilder.Entity<ProfileDislikedVideo>().HasData(
                new ProfileDislikedVideo { Id = 1, ProfileId = 1, VideoId = 4 },
                new ProfileDislikedVideo { Id = 2, ProfileId = 1, VideoId = 5 }
            );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Video> Videos { get; set; }

        public DbSet<VideoInfo> VideoInfos { get; set; }

        public DbSet<Visibility> Visibilities { get; set; }

        public DbSet<ProfileLikedVideo> ProfileLikedVideos { get; set; }

        public DbSet<ProfileDislikedVideo> ProfileDislikedVideos { get; set; }

        public DbSet<Avatar> Avatars { get; set; }
    }
}