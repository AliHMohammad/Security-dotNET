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
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Name);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Username = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<byte[]>(type: "longblob", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "longblob", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Username);
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
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_Users_user_username",
                        column: x => x.user_username,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Roles",
                column: "Name",
                values: new object[]
                {
                    "ADMIN",
                    "USER"
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "Email", "PasswordHash", "PasswordSalt" },
                values: new object[] { "Admin", "admin@kea.dk", new byte[] { 3, 243, 180, 78, 94, 151, 66, 113, 105, 133, 200, 176, 130, 150, 30, 223, 117, 192, 205, 198, 68, 158, 104, 215, 11, 255, 214, 83, 253, 72, 66, 223 }, new byte[] { 103, 144, 196, 119, 21, 146, 69, 144, 62, 208, 246, 129, 128, 80, 4, 149, 248, 78, 164, 116, 227, 11, 35, 188, 64, 41, 201, 178, 8, 103, 96, 135, 94, 160, 128, 0, 161, 64, 233, 175, 45, 184, 242, 46, 212, 36, 210, 165, 191, 193, 35, 203, 167, 245, 254, 213, 181, 9, 110, 38, 196, 127, 244, 153 } });

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
