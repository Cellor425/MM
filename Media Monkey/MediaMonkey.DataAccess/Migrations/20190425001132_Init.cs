using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaMonkey.DataAccess.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(nullable: false),
                    Size = table.Column<int>(nullable: false),
                    Format = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Visibilities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VisibilityName = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visibilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Username = table.Column<string>(maxLength: 30, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    IsEmailVerified = table.Column<bool>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastLogin = table.Column<DateTime>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false),
                    IsOwner = table.Column<bool>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Avatars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(maxLength: 255, nullable: true),
                    ContentType = table.Column<string>(maxLength: 100, nullable: true),
                    Content = table.Column<byte[]>(nullable: true),
                    ProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avatars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avatars_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommentContent = table.Column<string>(nullable: false),
                    ProfileId = table.Column<int>(nullable: true),
                    VideoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VideoInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    UploadDate = table.Column<DateTime>(nullable: false),
                    Likes = table.Column<int>(nullable: false),
                    Dislikes = table.Column<int>(nullable: false),
                    Views = table.Column<int>(nullable: false),
                    ThumbnailPath = table.Column<string>(nullable: true),
                    ProfileId = table.Column<int>(nullable: false),
                    VisibilityId = table.Column<int>(nullable: false),
                    VideoId = table.Column<int>(nullable: false),
                    IsUploadFinished = table.Column<bool>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoInfos", x => new { x.ProfileId, x.VideoId });
                    table.ForeignKey(
                        name: "FK_VideoInfos_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoInfos_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoInfos_Visibilities_VisibilityId",
                        column: x => x.VisibilityId,
                        principalTable: "Visibilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TagName = table.Column<string>(maxLength: 100, nullable: false),
                    VideoInfoProfileId = table.Column<int>(nullable: true),
                    VideoInfoVideoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_VideoInfos_VideoInfoProfileId_VideoInfoVideoId",
                        columns: x => new { x.VideoInfoProfileId, x.VideoInfoVideoId },
                        principalTable: "VideoInfos",
                        principalColumns: new[] { "ProfileId", "VideoId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CountryName" },
                values: new object[] { 1, "US" });

            migrationBuilder.InsertData(
                table: "Visibilities",
                columns: new[] { "Id", "VisibilityName" },
                values: new object[] { 1, "Public" });

            migrationBuilder.InsertData(
                table: "Visibilities",
                columns: new[] { "Id", "VisibilityName" },
                values: new object[] { 2, "Private" });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "Id", "CountryId", "CreationDate", "Description", "Email", "FirstName", "IsAdmin", "IsArchived", "IsEmailVerified", "IsOwner", "LastLogin", "LastName", "Password", "Username" },
                values: new object[] { 1, 1, new DateTime(2019, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "jdoe@gmail.com", "John", false, false, true, false, new DateTime(2019, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doe", "pass", "jDoe37" });

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_ProfileId",
                table: "Avatars",
                column: "ProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProfileId",
                table: "Comments",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_VideoId",
                table: "Comments",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_CountryId",
                table: "Profiles",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_VideoInfoProfileId_VideoInfoVideoId",
                table: "Tags",
                columns: new[] { "VideoInfoProfileId", "VideoInfoVideoId" });

            migrationBuilder.CreateIndex(
                name: "IX_VideoInfos_VideoId",
                table: "VideoInfos",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoInfos_VisibilityId",
                table: "VideoInfos",
                column: "VisibilityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avatars");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "VideoInfos");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Visibilities");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
