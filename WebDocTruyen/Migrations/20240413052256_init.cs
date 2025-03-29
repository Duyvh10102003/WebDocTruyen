using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDocTruyen.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoSuuTaps",
                columns: table => new
                {
                    BoSuuTapId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoSuuTaps", x => x.BoSuuTapId);
                });

            migrationBuilder.CreateTable(
                name: "LichSuXems",
                columns: table => new
                {
                    LichSuXemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichSuXems", x => x.LichSuXemId);
                });

            migrationBuilder.CreateTable(
                name: "TacGias",
                columns: table => new
                {
                    TacGiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTacGia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MetaTitle = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TacGias", x => x.TacGiaId);
                });

            migrationBuilder.CreateTable(
                name: "TheLoais",
                columns: table => new
                {
                    TheLoaiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTheLoai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MetaTitle = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheLoais", x => x.TheLoaiId);
                });

            migrationBuilder.CreateTable(
                name: "TrangThais",
                columns: table => new
                {
                    TrangThaiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrangThaiTruyen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MetaTitle = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangThais", x => x.TrangThaiId);
                });

            migrationBuilder.CreateTable(
                name: "Truyens",
                columns: table => new
                {
                    TruyenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTruyen = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Anh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetaTitle = table.Column<int>(type: "int", nullable: false),
                    LuotXem = table.Column<int>(type: "int", nullable: false),
                    TacGiaId = table.Column<int>(type: "int", nullable: true),
                    TheLoaiId = table.Column<int>(type: "int", nullable: true),
                    TrangThaiId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Truyens", x => x.TruyenId);
                    table.ForeignKey(
                        name: "FK_Truyens_TacGias_TacGiaId",
                        column: x => x.TacGiaId,
                        principalTable: "TacGias",
                        principalColumn: "TacGiaId");
                    table.ForeignKey(
                        name: "FK_Truyens_TheLoais_TheLoaiId",
                        column: x => x.TheLoaiId,
                        principalTable: "TheLoais",
                        principalColumn: "TheLoaiId");
                    table.ForeignKey(
                        name: "FK_Truyens_TrangThais_TrangThaiId",
                        column: x => x.TrangThaiId,
                        principalTable: "TrangThais",
                        principalColumn: "TrangThaiId");
                });

            migrationBuilder.CreateTable(
                name: "ChuongTruyens",
                columns: table => new
                {
                    ChuongTruyenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChuong = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetaTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    TruyenId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuongTruyens", x => x.ChuongTruyenId);
                    table.ForeignKey(
                        name: "FK_ChuongTruyens_Truyens_TruyenId",
                        column: x => x.TruyenId,
                        principalTable: "Truyens",
                        principalColumn: "TruyenId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChuongTruyens_TruyenId",
                table: "ChuongTruyens",
                column: "TruyenId");

            migrationBuilder.CreateIndex(
                name: "IX_Truyens_TacGiaId",
                table: "Truyens",
                column: "TacGiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Truyens_TheLoaiId",
                table: "Truyens",
                column: "TheLoaiId");

            migrationBuilder.CreateIndex(
                name: "IX_Truyens_TrangThaiId",
                table: "Truyens",
                column: "TrangThaiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoSuuTaps");

            migrationBuilder.DropTable(
                name: "ChuongTruyens");

            migrationBuilder.DropTable(
                name: "LichSuXems");

            migrationBuilder.DropTable(
                name: "Truyens");

            migrationBuilder.DropTable(
                name: "TacGias");

            migrationBuilder.DropTable(
                name: "TheLoais");

            migrationBuilder.DropTable(
                name: "TrangThais");
        }
    }
}
