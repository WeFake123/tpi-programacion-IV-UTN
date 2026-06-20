using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClientPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id_Plan",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "Id_Plan",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id_Plan",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "Id_Plan",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
