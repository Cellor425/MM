using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaMonkey.DataAccess.Migrations
{
    public partial class AddLikeAndDislikeTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "VideoInfos");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "VideoInfos");

            migrationBuilder.CreateTable(
                name: "ProfileDislikedVideos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProfileId = table.Column<int>(nullable: false),
                    VideoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileDislikedVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileDislikedVideos_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileDislikedVideos_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileLikedVideos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProfileId = table.Column<int>(nullable: false),
                    VideoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileLikedVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileLikedVideos_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileLikedVideos_Videos_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Videos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileDislikedVideos_ProfileId",
                table: "ProfileDislikedVideos",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileDislikedVideos_VideoId",
                table: "ProfileDislikedVideos",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileLikedVideos_ProfileId",
                table: "ProfileLikedVideos",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileLikedVideos_VideoId",
                table: "ProfileLikedVideos",
                column: "VideoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileDislikedVideos");

            migrationBuilder.DropTable(
                name: "ProfileLikedVideos");

            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "VideoInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "VideoInfos",
                nullable: false,
                defaultValue: 0);
        }
    }
}
