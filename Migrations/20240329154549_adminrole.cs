using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Security_CSharp.Migrations
{
    /// <inheritdoc />
    public partial class adminrole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_Roles_RolesName",
                table: "RoleUser");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_Users_UsersUsername",
                table: "RoleUser");

            migrationBuilder.RenameColumn(
                name: "UsersUsername",
                table: "RoleUser",
                newName: "role_name");

            migrationBuilder.RenameColumn(
                name: "RolesName",
                table: "RoleUser",
                newName: "user_username");

            migrationBuilder.RenameIndex(
                name: "IX_RoleUser_UsersUsername",
                table: "RoleUser",
                newName: "IX_RoleUser_role_name");

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "role_name", "user_username" },
                values: new object[] { "ADMIN", "Admin" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "Admin",
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 75, 196, 79, 128, 244, 127, 61, 193, 191, 42, 78, 10, 14, 141, 99, 230, 130, 9, 250, 28, 126, 203, 98, 248, 14, 191, 77, 134, 102, 254, 92, 214 }, new byte[] { 5, 145, 96, 86, 69, 244, 179, 178, 8, 66, 215, 78, 208, 0, 6, 124, 136, 252, 14, 198, 21, 23, 107, 207, 191, 48, 91, 4, 189, 183, 244, 67, 90, 46, 90, 46, 66, 112, 95, 215, 80, 43, 135, 21, 150, 138, 143, 141, 11, 202, 142, 3, 170, 253, 162, 200, 220, 184, 250, 147, 23, 234, 189, 159 } });

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_Roles_role_name",
                table: "RoleUser",
                column: "role_name",
                principalTable: "Roles",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_Users_user_username",
                table: "RoleUser",
                column: "user_username",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_Roles_role_name",
                table: "RoleUser");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleUser_Users_user_username",
                table: "RoleUser");

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "role_name", "user_username" },
                keyValues: new object[] { "ADMIN", "Admin" });

            migrationBuilder.RenameColumn(
                name: "role_name",
                table: "RoleUser",
                newName: "UsersUsername");

            migrationBuilder.RenameColumn(
                name: "user_username",
                table: "RoleUser",
                newName: "RolesName");

            migrationBuilder.RenameIndex(
                name: "IX_RoleUser_role_name",
                table: "RoleUser",
                newName: "IX_RoleUser_UsersUsername");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "Admin",
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 15, 105, 101, 107, 65, 239, 16, 237, 127, 69, 97, 119, 129, 52, 82, 90, 25, 112, 109, 23, 241, 93, 245, 68, 228, 192, 112, 41, 149, 137, 100, 155 }, new byte[] { 127, 56, 225, 129, 160, 228, 147, 212, 85, 25, 0, 4, 190, 198, 241, 61, 40, 43, 45, 217, 154, 95, 8, 241, 37, 34, 105, 132, 3, 66, 66, 211, 40, 43, 56, 155, 147, 129, 209, 181, 123, 29, 217, 120, 139, 239, 118, 56, 29, 8, 6, 146, 188, 141, 40, 137, 192, 105, 134, 86, 160, 18, 182, 149 } });

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_Roles_RolesName",
                table: "RoleUser",
                column: "RolesName",
                principalTable: "Roles",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUser_Users_UsersUsername",
                table: "RoleUser",
                column: "UsersUsername",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
