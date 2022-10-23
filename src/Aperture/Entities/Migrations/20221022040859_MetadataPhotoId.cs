using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aperture.Entities.Migrations
{
    public partial class MetadataPhotoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Photos_PhotoId",
                table: "Properties");

            migrationBuilder.AlterColumn<int>(
                name: "PhotoId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Photos_PhotoId",
                table: "Properties",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Photos_PhotoId",
                table: "Properties");

            migrationBuilder.AlterColumn<int>(
                name: "PhotoId",
                table: "Properties",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Photos_PhotoId",
                table: "Properties",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id");
        }
    }
}
