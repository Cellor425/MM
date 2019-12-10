using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaMonkey.DataAccess.Migrations
{
    public partial class AddVideoSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "Id", "Format", "Path", "Size" },
                values: new object[,]
                {
                    { 6, "video/mp4", "/videos/NTCpie1.MOV", 0 },
                    { 7, "video/mp4", "/videos/NTCpie2.MOV", 0 },
                    { 8, "video/mp4", "/videos/NTCpie3.MOV", 0 },
                    { 9, "video/mp4", "/videos/NTCpie4.MOV", 0 },
                    { 10, "video/mp4", "/videos/NTCpie5.MOV", 0 }
                });

            migrationBuilder.InsertData(
                table: "VideoInfos",
                columns: new[] { "ProfileId", "VideoId", "Description", "Id", "IsArchived", "IsUploadFinished", "ThumbnailPath", "Title", "UploadDate", "Views", "VisibilityId" },
                values: new object[,]
                {
                    { 1, 6, "Pie to the Face", 6, false, true, null, "NTCpie1", new DateTime(2019, 5, 1, 13, 23, 42, 0, DateTimeKind.Unspecified), 0, 1 },
                    { 1, 7, "Pie to the Face", 7, false, true, null, "NTCpie2", new DateTime(2019, 5, 1, 13, 23, 42, 0, DateTimeKind.Unspecified), 0, 1 },
                    { 1, 8, "Pie to the Face", 8, false, true, null, "NTCpie3", new DateTime(2019, 5, 1, 13, 23, 42, 0, DateTimeKind.Unspecified), 0, 1 },
                    { 1, 9, "Pie to the Face", 9, false, true, null, "NTCpie4", new DateTime(2019, 5, 1, 13, 23, 42, 0, DateTimeKind.Unspecified), 0, 1 },
                    { 1, 10, "Pie to the Face", 10, false, true, null, "NTCpie5", new DateTime(2019, 5, 1, 13, 23, 42, 0, DateTimeKind.Unspecified), 0, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VideoInfos",
                keyColumns: new[] { "ProfileId", "VideoId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "VideoInfos",
                keyColumns: new[] { "ProfileId", "VideoId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "VideoInfos",
                keyColumns: new[] { "ProfileId", "VideoId" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "VideoInfos",
                keyColumns: new[] { "ProfileId", "VideoId" },
                keyValues: new object[] { 1, 9 });

            migrationBuilder.DeleteData(
                table: "VideoInfos",
                keyColumns: new[] { "ProfileId", "VideoId" },
                keyValues: new object[] { 1, 10 });

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastLogin",
                value: new DateTime(2019, 5, 7, 10, 23, 6, 171, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "VideoInfos",
                keyColumns: new[] { "ProfileId", "VideoId" },
                keyValues: new object[] { 1, 3 },
                column: "UploadDate",
                value: new DateTime(2019, 5, 1, 13, 22, 48, 83, DateTimeKind.Unspecified));
        }
    }
}
