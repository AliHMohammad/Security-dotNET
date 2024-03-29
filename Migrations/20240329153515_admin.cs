using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Security_CSharp.Migrations
{
    /// <inheritdoc />
    public partial class admin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "Email", "PasswordHash", "PasswordSalt" },
                values: new object[] { "Admin", "Admin@kea.dk", new byte[] { 15, 105, 101, 107, 65, 239, 16, 237, 127, 69, 97, 119, 129, 52, 82, 90, 25, 112, 109, 23, 241, 93, 245, 68, 228, 192, 112, 41, 149, 137, 100, 155 }, new byte[] { 127, 56, 225, 129, 160, 228, 147, 212, 85, 25, 0, 4, 190, 198, 241, 61, 40, 43, 45, 217, 154, 95, 8, 241, 37, 34, 105, 132, 3, 66, 66, 211, 40, 43, 56, 155, 147, 129, 209, 181, 123, 29, 217, 120, 139, 239, 118, 56, 29, 8, 6, 146, 188, 141, 40, 137, 192, 105, 134, 86, 160, 18, 182, 149 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "Admin");
        }
    }
}
