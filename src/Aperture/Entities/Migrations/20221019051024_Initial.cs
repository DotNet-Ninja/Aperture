using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aperture.Entities.Migrations;

public partial class Initial : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Photos",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Slug = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                Title = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                FileName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                Caption = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                ExposureSummary = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                FullUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                LargeUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                SmallUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                ThumbnailUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                IconUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                Orientation = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                ContentType = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                DateUploaded = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Photos", x => x.Id)
                    .Annotation("SqlServer:Clustered", true);
            });

        migrationBuilder.CreateTable(
            name: "Tags",
            columns: table => new
            {
                Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tags", x => x.Name)
                    .Annotation("SqlServer:Clustered", true);
            });

        migrationBuilder.CreateTable(
            name: "Properties",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Tag = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                Value = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                PhotoId = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Properties", x => x.Id)
                    .Annotation("SqlServer:Clustered", true);
                table.ForeignKey(
                    name: "FK_Properties_Photos_PhotoId",
                    column: x => x.PhotoId,
                    principalTable: "Photos",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "PhotoTag",
            columns: table => new
            {
                PhotosId = table.Column<int>(type: "int", nullable: false),
                TagsName = table.Column<string>(type: "nvarchar(64)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PhotoTag", x => new { x.PhotosId, x.TagsName });
                table.ForeignKey(
                    name: "FK_PhotoTag_Photos_PhotosId",
                    column: x => x.PhotosId,
                    principalTable: "Photos",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_PhotoTag_Tags_TagsName",
                    column: x => x.TagsName,
                    principalTable: "Tags",
                    principalColumn: "Name",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "UK_Photo_Slug",
            table: "Photos",
            column: "Slug",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_PhotoTag_TagsName",
            table: "PhotoTag",
            column: "TagsName");

        migrationBuilder.CreateIndex(
            name: "IX_Properties_PhotoId",
            table: "Properties",
            column: "PhotoId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PhotoTag");

        migrationBuilder.DropTable(
            name: "Properties");

        migrationBuilder.DropTable(
            name: "Tags");

        migrationBuilder.DropTable(
            name: "Photos");
    }
}