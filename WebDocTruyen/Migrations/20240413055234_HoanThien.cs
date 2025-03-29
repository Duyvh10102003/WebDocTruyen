using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDocTruyen.Migrations
{
    /// <inheritdoc />
    public partial class HoanThien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TruyenId",
                table: "LichSuXems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "LichSuXems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TruyenId",
                table: "BoSuuTaps",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BoSuuTaps",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LichSuXems_TruyenId",
                table: "LichSuXems",
                column: "TruyenId");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuXems_UserId",
                table: "LichSuXems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BoSuuTaps_TruyenId",
                table: "BoSuuTaps",
                column: "TruyenId");

            migrationBuilder.CreateIndex(
                name: "IX_BoSuuTaps_UserId",
                table: "BoSuuTaps",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoSuuTaps_AspNetUsers_UserId",
                table: "BoSuuTaps",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoSuuTaps_Truyens_TruyenId",
                table: "BoSuuTaps",
                column: "TruyenId",
                principalTable: "Truyens",
                principalColumn: "TruyenId");

            migrationBuilder.AddForeignKey(
                name: "FK_LichSuXems_AspNetUsers_UserId",
                table: "LichSuXems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LichSuXems_Truyens_TruyenId",
                table: "LichSuXems",
                column: "TruyenId",
                principalTable: "Truyens",
                principalColumn: "TruyenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoSuuTaps_AspNetUsers_UserId",
                table: "BoSuuTaps");

            migrationBuilder.DropForeignKey(
                name: "FK_BoSuuTaps_Truyens_TruyenId",
                table: "BoSuuTaps");

            migrationBuilder.DropForeignKey(
                name: "FK_LichSuXems_AspNetUsers_UserId",
                table: "LichSuXems");

            migrationBuilder.DropForeignKey(
                name: "FK_LichSuXems_Truyens_TruyenId",
                table: "LichSuXems");

            migrationBuilder.DropIndex(
                name: "IX_LichSuXems_TruyenId",
                table: "LichSuXems");

            migrationBuilder.DropIndex(
                name: "IX_LichSuXems_UserId",
                table: "LichSuXems");

            migrationBuilder.DropIndex(
                name: "IX_BoSuuTaps_TruyenId",
                table: "BoSuuTaps");

            migrationBuilder.DropIndex(
                name: "IX_BoSuuTaps_UserId",
                table: "BoSuuTaps");

            migrationBuilder.DropColumn(
                name: "TruyenId",
                table: "LichSuXems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LichSuXems");

            migrationBuilder.DropColumn(
                name: "TruyenId",
                table: "BoSuuTaps");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BoSuuTaps");
        }
    }
}
