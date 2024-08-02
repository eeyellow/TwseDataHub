using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataShareHub.Migrations
{
    /// <inheritdoc />
    public partial class AddTable_Stocks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false, comment: "流水號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "名稱"),
                    Code = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "代碼"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "CONVERT(bit, 0)", comment: "是否刪除"),
                    CreateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "新增日期"),
                    UpdateDatetime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()", comment: "更新日期")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.ID);
                },
                comment: "上市個股");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fd32bc24-e001-4464-81fd-df792b5626f7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9e7cfb09-da6c-4bc8-b777-aa87b7257fb7", "AQAAAAIAAYagAAAAEGcNVVe5wJbsX/8mgf46ITGhcJ+1CMdJvXqF+h1K+dYcSrBsSiQ5QrKdGvqgZEr2rA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_IsDelete",
                table: "Stocks",
                column: "IsDelete");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fd32bc24-e001-4464-81fd-df792b5626f7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1b4ff66d-1910-4d06-8d33-e4391d018798", "AQAAAAIAAYagAAAAEKnfQXDw8sr1DIz6Fj/sCgLT4M73ukQvXgu0YuBk3Cd4fTJrB1QI3/H8V91gdQlxXw==" });
        }
    }
}
