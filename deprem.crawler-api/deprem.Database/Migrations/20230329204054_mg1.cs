using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace deprem.Database.Migrations
{
    /// <inheritdoc />
    public partial class mg1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Depremler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tarih = table.Column<DateTime>(type: "datetime2", nullable: true),
                    saat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    enlem = table.Column<double>(type: "float", nullable: false),
                    boylam = table.Column<double>(type: "float", nullable: false),
                    Location = table.Column<Point>(type: "geography", nullable: false),
                    derinlik = table.Column<double>(type: "float", nullable: false),
                    md = table.Column<double>(type: "float", nullable: false),
                    ml = table.Column<double>(type: "float", nullable: false),
                    mw = table.Column<double>(type: "float", nullable: false),
                    yer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cozumNiteliği = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depremler", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Depremler");
        }
    }
}
