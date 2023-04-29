using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPlanner.Context.MigrationsPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddIsReminderSentField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReminderSent",
                table: "tasks",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReminderSent",
                table: "tasks");
        }
    }
}
