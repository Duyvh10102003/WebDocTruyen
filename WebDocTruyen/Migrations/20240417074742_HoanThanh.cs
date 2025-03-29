using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDocTruyen.Migrations
{
    /// <inheritdoc />
    public partial class HoanThanh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoSuuTaps");

            migrationBuilder.DropTable(
                name: "LichSuXems");

            migrationBuilder.DropColumn(
                name: "LuotXem",
                table: "Truyens");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "Truyens");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "TrangThais");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "TheLoais");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "TacGias");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "ChuongTruyens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LuotXem",
                table: "Truyens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "Truyens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "TrangThais",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "TheLoais",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "TacGias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "ChuongTruyens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BoSuuTaps",
                columns: table => new
                {
                    BoSuuTapId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TruyenId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoSuuTaps", x => x.BoSuuTapId);
                    table.ForeignKey(
                        name: "FK_BoSuuTaps_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BoSuuTaps_Truyens_TruyenId",
                        column: x => x.TruyenId,
                        principalTable: "Truyens",
                        principalColumn: "TruyenId");
                });

            migrationBuilder.CreateTable(
                name: "LichSuXems",
                columns: table => new
                {
                    LichSuXemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TruyenId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichSuXems", x => x.LichSuXemId);
                    table.ForeignKey(
                        name: "FK_LichSuXems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LichSuXems_Truyens_TruyenId",
                        column: x => x.TruyenId,
                        principalTable: "Truyens",
                        principalColumn: "TruyenId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoSuuTaps_TruyenId",
                table: "BoSuuTaps",
                column: "TruyenId");

            migrationBuilder.CreateIndex(
                name: "IX_BoSuuTaps_UserId",
                table: "BoSuuTaps",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuXems_TruyenId",
                table: "LichSuXems",
                column: "TruyenId");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuXems_UserId",
                table: "LichSuXems",
                column: "UserId");
        }
    }
}
