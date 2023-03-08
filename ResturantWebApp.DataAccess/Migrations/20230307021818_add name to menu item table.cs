using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResturantWebApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addnametomenuitemtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MenuItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "MenuItems");
        }
    }
}
