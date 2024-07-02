using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenericSocialMedia.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRegisterdAtToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegisteredAt",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisteredAt",
                table: "Users");
        }
    }
}
