using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResturantWebApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_MenuItems_MenuTtemId",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "MenuTtemId",
                table: "OrderDetails",
                newName: "MenuItemId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_MenuTtemId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_MenuItemId");

            migrationBuilder.AlterColumn<string>(
                name: "PickupName",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_MenuItems_MenuItemId",
                table: "OrderDetails",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_MenuItems_MenuItemId",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "MenuItemId",
                table: "OrderDetails",
                newName: "MenuTtemId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_MenuItemId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_MenuTtemId");

            migrationBuilder.AlterColumn<string>(
                name: "PickupName",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_MenuItems_MenuTtemId",
                table: "OrderDetails",
                column: "MenuTtemId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
