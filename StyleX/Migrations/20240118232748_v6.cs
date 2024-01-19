using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StyleX.Migrations
{
    public partial class v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorB",
                table: "DesignInfo");

            migrationBuilder.DropColumn(
                name: "ColorG",
                table: "DesignInfo");

            migrationBuilder.DropColumn(
                name: "ColorR",
                table: "DesignInfo");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "DesignInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "DesignInfo");

            migrationBuilder.AddColumn<double>(
                name: "ColorB",
                table: "DesignInfo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ColorG",
                table: "DesignInfo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ColorR",
                table: "DesignInfo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
