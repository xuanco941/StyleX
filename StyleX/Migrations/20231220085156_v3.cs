using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StyleX.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Order",
                newName: "PercentSale");

            migrationBuilder.AddColumn<double>(
                name: "BasePrice",
                table: "Order",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NetPrice",
                table: "Order",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasePrice",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "NetPrice",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "PercentSale",
                table: "Order",
                newName: "Total");
        }
    }
}
