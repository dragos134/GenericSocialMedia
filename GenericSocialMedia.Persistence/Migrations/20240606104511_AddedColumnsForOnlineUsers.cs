using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenericSocialMedia.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedColumnsForOnlineUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChatConnectionId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatConnectionId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsOnline",
                table: "Users");
        }
    }
}
