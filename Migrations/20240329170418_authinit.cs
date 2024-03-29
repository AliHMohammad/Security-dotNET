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
                values: new object[] { "Admin", "admin@kea.dk", new byte[] { 195, 98, 110, 174, 131, 11, 250, 48, 81, 27, 23, 107, 209, 220, 231, 180, 84, 123, 61, 164, 241, 74, 148, 44, 195, 151, 247, 94, 175, 100, 65, 128 }, new byte[] { 11, 145, 241, 194, 113, 155, 124, 62, 184, 27, 23, 252, 87, 157, 140, 70, 142, 210, 38, 83, 124, 180, 38, 159, 131, 170, 32, 237, 112, 231, 192, 46, 140, 135, 109, 229, 224, 50, 226, 190, 66, 222, 118, 182, 32, 132, 141, 92, 47, 156, 42, 151, 45, 241, 244, 139, 214, 70, 237, 0, 106, 45, 119, 140 } });

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
