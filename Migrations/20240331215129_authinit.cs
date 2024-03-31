using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Security_CSharp.Migrations
{
    /// <inheritdoc />
    public partial class authinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.name);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    username = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password_hash = table.Column<byte[]>(type: "longblob", nullable: false),
                    password_salt = table.Column<byte[]>(type: "longblob", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.username);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    user_username = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    role_name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.user_username, x.role_name });
                    table.ForeignKey(
                        name: "FK_RoleUser_Roles_role_name",
                        column: x => x.role_name,
                        principalTable: "Roles",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_Users_user_username",
                        column: x => x.user_username,
                        principalTable: "Users",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Roles",
                column: "name",
                values: new object[]
                {
                    "ADMIN",
                    "USER"
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "username", "CreatedAt", "email", "password_hash", "password_salt" },
                values: new object[] { "Admin", new DateTime(2024, 3, 31, 23, 51, 28, 811, DateTimeKind.Local).AddTicks(862), "admin@kea.dk", new byte[] { 104, 150, 192, 42, 149, 165, 109, 242, 220, 225, 227, 195, 10, 63, 127, 9, 102, 100, 46, 155, 27, 153, 137, 108, 23, 255, 19, 143, 115, 211, 102, 125 }, new byte[] { 150, 55, 9, 229, 163, 127, 81, 230, 230, 198, 107, 114, 191, 247, 18, 107, 49, 226, 149, 36, 118, 14, 251, 175, 37, 255, 199, 114, 61, 15, 202, 132, 87, 105, 203, 4, 0, 152, 141, 215, 69, 65, 178, 180, 223, 15, 0, 124, 228, 41, 11, 52, 85, 181, 73, 187, 109, 180, 160, 40, 190, 26, 27, 169 } });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "role_name", "user_username" },
                values: new object[] { "ADMIN", "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_role_name",
                table: "RoleUser",
                column: "role_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
