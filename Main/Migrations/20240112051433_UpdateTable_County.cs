using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace TwseDataHub.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTable_County : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EngName",
                table: "County",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "英文名稱");

            migrationBuilder.AddColumn<Geometry>(
                name: "Geom",
                table: "County",
                type: "geometry",
                nullable: true,
                comment: "座標點位");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EngName",
                table: "County");

            migrationBuilder.DropColumn(
                name: "Geom",
                table: "County");
        }
    }
}
