﻿// <auto-generated />
using System;
using MediaMonkey.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MediaMonkey.DataAccess.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190430185520_AddLikeAndDislikeTables")]
    partial class AddLikeAndDislikeTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MediaMonkey.DataAccess.Models.Avatar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Content");

                    b.Property<string>("ContentType")
                        .HasMaxLength(100);

                    b.Property<string>("FileName")
                        .HasMaxLength(255);

                    b.Property<int>("ProfileId");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId")
                        .IsUnique();

                    b.ToTable("Avatars");
                });

            modelBuilder.Entity("MediaMonkey.DataAccess.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommentContent")
                        .IsRequired();

                    b.Property<int?>("ProfileId");

                    b.Property<int?>("VideoId");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.HasIndex("VideoId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("MediaMonkey.DataAccess.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Countries");

                    b.HasData(
                        new { Id = 1, CountryName = "US" }
                    );
                });

            modelBuilder.Entity("MediaMonkey.DataAccess.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CountryId")
                        .IsRequired();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("IsArchived");

                    b.Property<bool>("IsEmailVerified");

                    b.Property<bool>("IsOwner");

                    b.Property<DateTime>("LastLogin");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Profiles");

                    b.HasData(
                        new { Id = 1, CountryId = 1, CreationDate = new DateTime(2019, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), Email = "jdoe@gmail.com", FirstName = "John", IsAdmin = false, IsArchived = false, IsEmailVerified = true, IsOwner = false, LastLogin = new DateTime(2019, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), LastName = "Doe", Password = "pass", Username = "jDoe37" }
                    );
                });

            modelBuilder.Entity("MediaMonkey.DataAccess.Models.ProfileDislikedVideo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProfileId");

                    b.Property<int>("VideoId");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.HasIndex("VideoId");

                    b.ToTable("ProfileDislikedVideos");
                });

            modelBuilder.Entity("MediaMonkey.DataAccess.Models.ProfileLikedVideo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProfileId");

                    b.Property<int>("VideoId");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.HasIndex("VideoId");

                    b.ToTable("ProfileLikedVideos");
                });

            modelBuilder.Entity("MediaMonkey.DataAccess.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int?>("VideoInfoProfileId");

                    b.Property<int?>("VideoInfoVideoId");

                    b.HasKey("Id");

                    b.HasIndex("VideoInfoProfileId", "VideoInfoVideoId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("MediaMonkey.DataAccess.Models.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Format")
                        .IsRequired();

                    b.Property<string>("Path")
                        .IsRequired();

                    b.Property<int>("Size");

                    b.HasKey("Id");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("MediaMonkey.DataAccess.Models.VideoInfo", b =>
                {
                    b.Property<int>("ProfileId");

                    b.Property<int>("VideoId");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsArchived");

                    b.Property<bool>("IsUploadFinished");

                    b.Property<string>("ThumbnailPath");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("UploadDate");

                    b.Property<int>("Views");

                    b.Property<int?>("VisibilityId")
                        .IsRequired();

                    b.HasKey("ProfileId", "VideoId");

                    b.HasIndex("VideoId");

                    b.HasIndex("VisibilityId");

                    b.ToTable("VideoInfos");
                });

            modelBuilder.Entity("MediaMonkey.DataAccess.Models.Visibility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("VisibilityName")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.ToTable("Visibilities");

                    b.HasData(
                        new { Id = 1, VisibilityName = "Public" },
                        new { Id = 2, VisibilityName = "Private" }
                    );
                });

            modelBuilder.Entity("MediaMonkey.DataAccess.Models.Avatar", b =>
                {
                    b.HasOne("MediaMonkey.DataAccess.Models.Profile", "Profile")
                        .WithOne("Avatar")
                        .HasForeignKey("MediaMonkey.DataAccess.Models.Avatar", "ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MediaMonkey.DataAccess.Models.Comment", b =>
                {
                    b.HasOne("MediaMonkey.DataAccess.Models.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId");

                    b.HasOne("MediaMonkey.DataAccess.Models.Video")
                        .WithMany("Comments")
                        .HasForeignKey("VideoId");
                });

            modelBuilder.Entity("MediaMonkey.DataAccess.Models.Profile", b =>
                {
                    b.HasOne("MediaMonkey.DataAccess.Models.Country", "Country")
                        .WithMany("Profiles")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MediaMonkey.DataAccess.Models.ProfileDislikedVideo", b =>
                {
                    b.HasOne("MediaMonkey.DataAccess.Models.Profile", "Profile")
                        .WithMany("DislikedVideos")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MediaMonkey.DataAccess.Models.Video", "Video")
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MediaMonkey.DataAccess.Models.ProfileLikedVideo", b =>
                {
                    b.HasOne("MediaMonkey.DataAccess.Models.Profile", "Profile")
                        .WithMany("LikedVideos")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MediaMonkey.DataAccess.Models.Video", "Video")
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MediaMonkey.DataAccess.Models.Tag", b =>
                {
                    b.HasOne("MediaMonkey.DataAccess.Models.VideoInfo")
                        .WithMany("Tags")
                        .HasForeignKey("VideoInfoProfileId", "VideoInfoVideoId");
                });

            modelBuilder.Entity("MediaMonkey.DataAccess.Models.VideoInfo", b =>
                {
                    b.HasOne("MediaMonkey.DataAccess.Models.Profile", "Profile")
                        .WithMany("VideoInfos")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MediaMonkey.DataAccess.Models.Video", "Video")
                        .WithMany("VideoInfos")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MediaMonkey.DataAccess.Models.Visibility", "Visibility")
                        .WithMany("VideoInfos")
                        .HasForeignKey("VisibilityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}