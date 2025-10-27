using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LandingAppFolhetos.Migrations
{
    /// <inheritdoc />
    public partial class AddRegistoLeituraFolheto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegistoLeituraFolhetos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdLocal = table.Column<int>(type: "INTEGER", nullable: false),
                    IdUtilizador = table.Column<string>(type: "TEXT", nullable: false),
                    Leituras = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistoLeituraFolhetos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistoLeituraFolhetos_AspNetUsers_IdUtilizador",
                        column: x => x.IdUtilizador,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistoLeituraFolhetos_IdUtilizador",
                table: "RegistoLeituraFolhetos",
                column: "IdUtilizador");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistoLeituraFolhetos");
        }
    }
}
