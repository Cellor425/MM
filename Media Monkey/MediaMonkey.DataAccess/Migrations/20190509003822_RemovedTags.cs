using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaMonkey.DataAccess.Migrations
{
    public partial class RemovedTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Profiles_ProfileId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileId",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastLogin",
                value: new DateTime(2019, 5, 8, 19, 38, 21, 939, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "VideoInfos",
                keyColumns: new[] { "ProfileId", "VideoId" },
                keyValues: new object[] { 1, 3 },
                column: "UploadDate",
                value: new DateTime(2019, 5, 1, 13, 22, 48, 83, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Profiles_ProfileId",
                table: "Comments",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Profiles_ProfileId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 1,
                column: "LastLogin",
                value: new DateTime(2019, 5, 8, 16, 41, 47, 187, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "VideoInfos",
                keyColumns: new[] { "ProfileId", "VideoId" },
                keyValues: new object[] { 1, 3 },
                column: "UploadDate",
                value: new DateTime(2019, 5, 1, 13, 22, 48, 83, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Profiles_ProfileId",
                table: "Comments",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
