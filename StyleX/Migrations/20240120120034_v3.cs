using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StyleX.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "OrientationX",
                table: "DecalInfo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OrientationY",
                table: "DecalInfo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OrientationZ",
                table: "DecalInfo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrientationX",
                table: "DecalInfo");

            migrationBuilder.DropColumn(
                name: "OrientationY",
                table: "DecalInfo");

            migrationBuilder.DropColumn(
                name: "OrientationZ",
                table: "DecalInfo");
        }
    }
}
