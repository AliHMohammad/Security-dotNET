using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Security_CSharp.Migrations
{
    /// <inheritdoc />
    public partial class mssqlinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password_hash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    password_salt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.username);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    user_username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    role_name = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                });

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
                values: new object[] { "Admin", new DateTime(2024, 4, 7, 22, 21, 38, 162, DateTimeKind.Local).AddTicks(5091), "admin@kea.dk", new byte[] { 68, 207, 141, 99, 35, 158, 219, 51, 223, 218, 149, 32, 244, 186, 208, 103, 160, 66, 196, 145, 23, 27, 176, 80, 96, 236, 84, 24, 132, 224, 82, 25 }, new byte[] { 140, 47, 214, 69, 227, 201, 230, 182, 208, 214, 236, 134, 52, 251, 100, 116, 67, 32, 187, 61, 173, 120, 41, 249, 142, 253, 2, 235, 191, 181, 167, 29, 191, 65, 194, 77, 230, 135, 112, 232, 252, 28, 135, 156, 243, 197, 220, 157, 146, 240, 76, 61, 122, 63, 150, 109, 15, 252, 33, 37, 135, 102, 159, 24 } });

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
