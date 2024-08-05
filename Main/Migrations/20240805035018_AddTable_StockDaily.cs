using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwseDataHub.Migrations
{
    /// <inheritdoc />
    public partial class AddTable_StockDaily : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockDaily",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false, comment: "流水號")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "代碼"),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "交易日期"),
                    TradeVolume = table.Column<long>(type: "bigint", nullable: true, comment: "成交量"),
                    TradeValue = table.Column<long>(type: "bigint", nullable: true, comment: "成交值"),
                    OpeningPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: true, comment: "開盤價"),
                    HighestPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: true, comment: "盤中最高價"),
                    LowestPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: true, comment: "盤中最低價"),
                    ClosingPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: true, comment: "收盤價"),
                    Change = table.Column<decimal>(type: "decimal(18,4)", nullable: true, comment: "漲跌價差"),
                    TransactionCount = table.Column<long>(type: "bigint", nullable: true, comment: "成交筆數")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockDaily", x => x.ID);
                },
                comment: "上市個股當日成交資訊");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fd32bc24-e001-4464-81fd-df792b5626f7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "65e438b1-3bd3-48fc-92bc-a7c544874e98", "AQAAAAIAAYagAAAAEOt9t92d3rFCI/79pkAppm4uR727QpovmAttqu1aiKEuzE/nMVGuNKJCBWFbW8incg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockDaily");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fd32bc24-e001-4464-81fd-df792b5626f7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6300588d-a0f0-45e5-98ef-b41bf4cfa0f5", "AQAAAAIAAYagAAAAEL2WSmZy2txeZLBIxS6bNecIa5/08rP66OOOhnGdAg/3nKqIO/nc8kgjZvMqIQnz9g==" });
        }
    }
}
