using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StyleX.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplacementMap",
                table: "ProductSetting");

            migrationBuilder.DropColumn(
                name: "Map",
                table: "ProductSetting");

            migrationBuilder.DropColumn(
                name: "TextureRotation",
                table: "DesignInfo");

            migrationBuilder.RenameColumn(
                name: "ImageMaterial",
                table: "DesignInfo",
                newName: "RoughnessMap");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "DesignInfo",
                newName: "NormalMap");

            migrationBuilder.AddColumn<string>(
                name: "AoMap",
                table: "DesignInfo",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "MetalnessMap",
                table: "DesignInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameMaterial",
                table: "DesignInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AoMap",
                table: "DesignInfo");

            migrationBuilder.DropColumn(
                name: "ColorB",
                table: "DesignInfo");

            migrationBuilder.DropColumn(
                name: "ColorG",
                table: "DesignInfo");

            migrationBuilder.DropColumn(
                name: "ColorR",
                table: "DesignInfo");

            migrationBuilder.DropColumn(
                name: "MetalnessMap",
                table: "DesignInfo");

            migrationBuilder.DropColumn(
                name: "NameMaterial",
                table: "DesignInfo");

            migrationBuilder.RenameColumn(
                name: "RoughnessMap",
                table: "DesignInfo",
                newName: "ImageMaterial");

            migrationBuilder.RenameColumn(
                name: "NormalMap",
                table: "DesignInfo",
                newName: "Color");

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

            migrationBuilder.AddColumn<double>(
                name: "TextureRotation",
                table: "DesignInfo",
                type: "float",
                nullable: true);
        }
    }
}
