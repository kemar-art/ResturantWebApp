using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResturantWebApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updatepaynetname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "OrderHeaders",
                newName: "PaymentIntentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentIntentId",
                table: "OrderHeaders",
                newName: "PaymentId");
        }
    }
}
