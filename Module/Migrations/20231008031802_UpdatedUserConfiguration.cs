using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Module.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUser_IdentityUser_IdentityUserId",
                table: "IdentityUser");

            migrationBuilder.DropIndex(
                name: "IX_IdentityUser_IdentityUserId",
                table: "IdentityUser");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "IdentityUser");

            migrationBuilder.CreateTable(
                name: "UserFriends",
                columns: table => new
                {
                    FriendsId = table.Column<int>(type: "int", nullable: false),
                    IdentityUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFriends", x => new { x.FriendsId, x.IdentityUserId });
                    table.ForeignKey(
                        name: "FK_UserFriends_IdentityUser_FriendsId",
                        column: x => x.FriendsId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFriends_IdentityUser_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserFriends_IdentityUserId",
                table: "UserFriends",
                column: "IdentityUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFriends");

            migrationBuilder.AddColumn<int>(
                name: "IdentityUserId",
                table: "IdentityUser",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUser_IdentityUserId",
                table: "IdentityUser",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUser_IdentityUser_IdentityUserId",
                table: "IdentityUser",
                column: "IdentityUserId",
                principalTable: "IdentityUser",
                principalColumn: "Id");
        }
    }
}
