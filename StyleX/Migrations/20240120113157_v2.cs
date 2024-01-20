using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StyleX.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "DecalInfo",
                newName: "MeshUuid");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "DecalInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "PositionX",
                table: "DecalInfo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PositionY",
                table: "DecalInfo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PositionZ",
                table: "DecalInfo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "RenderOrder",
                table: "DecalInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Size",
                table: "DecalInfo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "DecalInfo");

            migrationBuilder.DropColumn(
                name: "PositionX",
                table: "DecalInfo");

            migrationBuilder.DropColumn(
                name: "PositionY",
                table: "DecalInfo");

            migrationBuilder.DropColumn(
                name: "PositionZ",
                table: "DecalInfo");

            migrationBuilder.DropColumn(
                name: "RenderOrder",
                table: "DecalInfo");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "DecalInfo");

            migrationBuilder.RenameColumn(
                name: "MeshUuid",
                table: "DecalInfo",
                newName: "ImageUrl");
        }
    }
}
