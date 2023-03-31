using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResturantWebApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changetransationidtosessionidandaddpaymentid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "OrderHeaders",
                newName: "SessionId");

            migrationBuilder.AddColumn<string>(
                name: "PaymentId",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "OrderHeaders");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "OrderHeaders",
                newName: "TransactionId");
        }
    }
}
