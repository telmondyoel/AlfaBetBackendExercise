using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlfaBetBackendExercise.Migrations
{
    /// <inheritdoc />
    public partial class Events_AddedColumnForReminderTriggered : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ReminderTriggered",
                table: "Events",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReminderTriggered",
                table: "Events");
        }
    }
}
