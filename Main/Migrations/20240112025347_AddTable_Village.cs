using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace DataShareHub.Migrations
{
    /// <inheritdoc />
    public partial class AddTable_Village : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Village",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false, comment: "流水號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "名稱"),
                    VillageCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "村里代碼"),
                    CountyName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "縣市名稱"),
                    CountyID = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false, comment: "縣市代號"),
                    CountyCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "縣市代碼"),
                    TownName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "鄉鎮市區名稱"),
                    TownID = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false, comment: "鄉鎮市區代號"),
                    TownCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "鄉鎮市區代碼"),
                    EngName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "英文名稱"),
                    Note = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "備註"),
                    Geom = table.Column<Geometry>(type: "geometry", nullable: true, comment: "座標點位"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "CONVERT(bit, 0)", comment: "是否刪除"),
                    CreateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "新增日期"),
                    UpdateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "更新日期")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Village", x => x.ID);
                },
                comment: "村里");

            migrationBuilder.CreateIndex(
                name: "IX_Village_IsDelete",
                table: "Village",
                column: "IsDelete");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Village");
        }
    }
}
