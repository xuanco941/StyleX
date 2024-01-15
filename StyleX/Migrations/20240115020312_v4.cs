using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StyleX.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AoMap",
                table: "ProductSetting",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DisplacementMap",
                table: "ProductSetting",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Map",
                table: "ProductSetting",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MetalnessMap",
                table: "ProductSetting",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NormalMap",
                table: "ProductSetting",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoughnessMap",
                table: "ProductSetting",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AoMap",
                table: "ProductSetting");

            migrationBuilder.DropColumn(
                name: "DisplacementMap",
                table: "ProductSetting");

            migrationBuilder.DropColumn(
                name: "Map",
                table: "ProductSetting");

            migrationBuilder.DropColumn(
                name: "MetalnessMap",
                table: "ProductSetting");

            migrationBuilder.DropColumn(
                name: "NormalMap",
                table: "ProductSetting");

            migrationBuilder.DropColumn(
                name: "RoughnessMap",
                table: "ProductSetting");
        }
    }
}
