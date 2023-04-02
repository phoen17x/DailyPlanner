using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPlanner.Context.MigrationsPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AssociateNotebooksWithUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "notebooks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_notebooks_UserId",
                table: "notebooks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_notebooks_users_UserId",
                table: "notebooks",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notebooks_users_UserId",
                table: "notebooks");

            migrationBuilder.DropIndex(
                name: "IX_notebooks_UserId",
                table: "notebooks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "notebooks");
        }
    }
}
