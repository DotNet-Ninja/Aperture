using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aperture.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Href = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    OpensInNewTab = table.Column<bool>(type: "bit", nullable: false),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItem", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_MenuItems_MenuItems_ParentId",
                        column: x => x.ParentId,
                        principalTable: "MenuItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Name)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(384)", maxLength: 384, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    AvatarId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    RolesName = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.RolesName, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RolesName",
                        column: x => x.RolesName,
                        principalTable: "Roles",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Href", "OpensInNewTab", "ParentId", "SortIndex", "Text" },
                values: new object[,]
                {
                    { 1, "/Posts", false, null, 1, "Posts" },
                    { 2, "/Galleries", false, null, 2, "Galleries" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                column: "Name",
                values: new object[]
                {
                    "Contributor",
                    "Family",
                    "Friend",
                    "Owner"
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_ParentId",
                table: "MenuItems",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UsersId",
                table: "UserRoles",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
