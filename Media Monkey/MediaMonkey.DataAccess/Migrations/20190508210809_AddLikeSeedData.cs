using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaMonkey.DataAccess.Migrations
{
    public partial class AddLikeSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProfileDislikedVideos",
                columns: new[] { "Id", "ProfileId", "VideoId" },
                values: new object[,]
                {
                    { 1, 1, 2 },
                    { 2, 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "ProfileLikedVideos",
                columns: new[] { "Id", "ProfileId", "VideoId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 }
                });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastLogin",
                value: new DateTime(2019, 5, 8, 16, 8, 9, 67, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "VideoInfos",
                keyColumns: new[] { "ProfileId", "VideoId" },
                keyValues: new object[] { 1, 3 },
                column: "UploadDate",
                value: new DateTime(2019, 5, 1, 13, 22, 48, 83, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProfileDislikedVideos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProfileDislikedVideos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProfileLikedVideos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProfileLikedVideos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastLogin",
                value: new DateTime(2019, 5, 7, 20, 29, 45, 329, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "VideoInfos",
                keyColumns: new[] { "ProfileId", "VideoId" },
                keyValues: new object[] { 1, 3 },
                column: "UploadDate",
                value: new DateTime(2019, 5, 1, 13, 22, 48, 83, DateTimeKind.Unspecified));
        }
    }
}
