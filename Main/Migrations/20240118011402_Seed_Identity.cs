using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TwseDataHub.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Identity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a2dbb33e-1ef6-46c1-961c-4ae1e4e0931c", null, "User", "USER" },
                    { "e3f7b320-0e40-48c2-9bcc-84e62c36e6d7", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "fd32bc24-e001-4464-81fd-df792b5626f7", 0, "1b4ff66d-1910-4d06-8d33-e4391d018798", "admin@your.company.tw", false, false, null, "ADMIN@YOUR.COMPANY.TW", "ADMIN", "AQAAAAIAAYagAAAAEKnfQXDw8sr1DIz6Fj/sCgLT4M73ukQvXgu0YuBk3Cd4fTJrB1QI3/H8V91gdQlxXw==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "a2dbb33e-1ef6-46c1-961c-4ae1e4e0931c", "fd32bc24-e001-4464-81fd-df792b5626f7" },
                    { "e3f7b320-0e40-48c2-9bcc-84e62c36e6d7", "fd32bc24-e001-4464-81fd-df792b5626f7" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a2dbb33e-1ef6-46c1-961c-4ae1e4e0931c", "fd32bc24-e001-4464-81fd-df792b5626f7" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e3f7b320-0e40-48c2-9bcc-84e62c36e6d7", "fd32bc24-e001-4464-81fd-df792b5626f7" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2dbb33e-1ef6-46c1-961c-4ae1e4e0931c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3f7b320-0e40-48c2-9bcc-84e62c36e6d7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fd32bc24-e001-4464-81fd-df792b5626f7");
        }
    }
}
