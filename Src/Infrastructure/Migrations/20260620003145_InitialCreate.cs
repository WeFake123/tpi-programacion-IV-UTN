using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Plans_Id_Plan",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Id_Plan",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Id_Plan",
                table: "Users",
                column: "Id_Plan");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Plans_Id_Plan",
                table: "Users",
                column: "Id_Plan",
                principalTable: "Plans",
                principalColumn: "Id");
        }
    }
}
