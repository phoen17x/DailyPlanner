using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPlanner.Context.MigrationsPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_authors_NotebookId",
                table: "tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_authors",
                table: "authors");

            migrationBuilder.RenameTable(
                name: "authors",
                newName: "notebooks");

            migrationBuilder.RenameIndex(
                name: "IX_authors_Uid",
                table: "notebooks",
                newName: "IX_notebooks_Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_notebooks",
                table: "notebooks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_notebooks_NotebookId",
                table: "tasks",
                column: "NotebookId",
                principalTable: "notebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_notebooks_NotebookId",
                table: "tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_notebooks",
                table: "notebooks");

            migrationBuilder.RenameTable(
                name: "notebooks",
                newName: "authors");

            migrationBuilder.RenameIndex(
                name: "IX_notebooks_Uid",
                table: "authors",
                newName: "IX_authors_Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_authors",
                table: "authors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_authors_NotebookId",
                table: "tasks",
                column: "NotebookId",
                principalTable: "authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
