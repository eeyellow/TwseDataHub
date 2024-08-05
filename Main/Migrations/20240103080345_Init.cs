using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwseDataHub.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "County",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false, comment: "流水號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "名稱"),
                    CountyID = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false, comment: "縣市代號"),
                    CountyCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, comment: "縣市代碼"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "CONVERT(bit, 0)", comment: "是否刪除"),
                    CreateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "新增日期"),
                    UpdateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "更新日期")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_County", x => x.ID);
                },
                comment: "縣市");

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false, comment: "流水號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "名稱"),
                    Account = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "帳號"),
                    Password = table.Column<string>(type: "nvarchar(MAX)", nullable: false, comment: "密碼"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "CONVERT(bit, 0)", comment: "是否刪除"),
                    CreateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "新增日期"),
                    UpdateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "更新日期")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "County");

            migrationBuilder.DropTable(
                name: "UserProfile");
        }
    }
}
