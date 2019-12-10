using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaMonkey.DataAccess.Migrations
{
    public partial class AddSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CountryName" },
                values: new object[] { 2, "Canda" });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "Description", "IsAdmin", "IsOwner", "LastLogin", "Password" },
                values: new object[] { new DateTime(2019, 5, 1, 13, 14, 4, 0, DateTimeKind.Unspecified), "Joe is the master.", true, true, new DateTime(2019, 5, 7, 10, 23, 6, 171, DateTimeKind.Local), "AQAAAAEAACcQAAAAEKQ1ccM69zyRO+RbwtB3muSaviqjcj+kXa9Ee7Ou3BWhg7YxKkVr8PF8YttEv/ypeQ==" });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "Id", "Format", "Path", "Size" },
                values: new object[,]
                {
                    { 1, "video/mp4", "/videos/Color.mp4", 8883922 },
                    { 2, "video/mp4", "/videos/Wausau.mp4", 7712840 },
                    { 3, "video/mp4", "/videos/Test3.mp4", 5253880 },
                    { 4, "video/mp4", "/videos/Gone Fishing.mp4", 9168849 },
                    { 5, "video/mp4", "/videos/Higher - Phantom 3 Standard.mp4", 23374987 }
                });

            migrationBuilder.InsertData(
                table: "VideoInfos",
                columns: new[] { "ProfileId", "VideoId", "Description", "Id", "IsArchived", "IsUploadFinished", "ThumbnailPath", "Title", "UploadDate", "Views", "VisibilityId" },
                values: new object[,]
                {
                    { 1, 1, "A colorful video reel with some music.", 1, false, true, null, "Colors", new DateTime(2019, 5, 1, 13, 19, 45, 0, DateTimeKind.Unspecified), 0, 1 },
                    { 1, 2, "A video showing some area around Wausau, WI.", 2, false, true, null, "Unfinished", new DateTime(2019, 5, 1, 13, 21, 41, 0, DateTimeKind.Unspecified), 0, 2 },
                    { 1, 3, "", 3, false, false, null, "Unfinished", new DateTime(2019, 5, 1, 13, 22, 48, 83, DateTimeKind.Unspecified), 0, 2 },
                    { 1, 4, "A video of some fishing spots.", 4, false, true, null, "Gone Fishing", new DateTime(2019, 5, 1, 13, 23, 3, 0, DateTimeKind.Unspecified), 0, 1 },
                    { 1, 5, "What?", 5, false, true, null, "Higher- Phantom 3 Standard", new DateTime(2019, 5, 1, 13, 23, 42, 0, DateTimeKind.Unspecified), 0, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "VideoInfos",
                keyColumns: new[] { "ProfileId", "VideoId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "VideoInfos",
                keyColumns: new[] { "ProfileId", "VideoId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "VideoInfos",
                keyColumns: new[] { "ProfileId", "VideoId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "VideoInfos",
                keyColumns: new[] { "ProfileId", "VideoId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "VideoInfos",
                keyColumns: new[] { "ProfileId", "VideoId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 5);

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

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "Description", "IsAdmin", "IsOwner", "LastLogin", "Password" },
                values: new object[] { new DateTime(2019, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, new DateTime(2019, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "pass" });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_VideoInfoProfileId_VideoInfoVideoId",
                table: "Tags",
                columns: new[] { "VideoInfoProfileId", "VideoInfoVideoId" });
        }
    }
}
