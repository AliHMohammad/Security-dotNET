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
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                columns: new[] { "username", "created_at", "email", "password_hash", "password_salt" },
                values: new object[] { "Admin", new DateTime(2024, 4, 4, 0, 23, 5, 626, DateTimeKind.Local).AddTicks(6639), "admin@kea.dk", new byte[] { 118, 32, 177, 51, 119, 123, 86, 224, 16, 251, 196, 0, 95, 95, 188, 85, 223, 81, 247, 189, 174, 168, 230, 60, 167, 20, 213, 232, 54, 207, 6, 2 }, new byte[] { 233, 166, 7, 229, 89, 240, 79, 179, 118, 35, 224, 96, 147, 219, 217, 124, 17, 243, 186, 23, 238, 149, 167, 251, 136, 78, 105, 173, 228, 4, 135, 130, 76, 49, 194, 114, 174, 150, 48, 177, 218, 148, 33, 167, 190, 44, 126, 90, 241, 86, 162, 133, 37, 3, 150, 71, 184, 238, 250, 62, 176, 89, 120, 26 } });

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
