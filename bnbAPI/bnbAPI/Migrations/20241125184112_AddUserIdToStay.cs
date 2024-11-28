using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bnbAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToStay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Stays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stays_UserId",
                table: "Stays",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stays_Users_UserId",
                table: "Stays",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stays_Users_UserId",
                table: "Stays");

            migrationBuilder.DropIndex(
                name: "IX_Stays_UserId",
                table: "Stays");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Stays");
        }
    }
}
