using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tracker.Migrations
{
    /// <inheritdoc />
    public partial class AddWfhAndPermissionHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "WorkLogs",
                newName: "TaskDescription");

            migrationBuilder.AddColumn<int>(
                name: "PermissionHours",
                table: "WorkLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "WfhRequest",
                table: "WorkLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermissionHours",
                table: "WorkLogs");

            migrationBuilder.DropColumn(
                name: "WfhRequest",
                table: "WorkLogs");

            migrationBuilder.RenameColumn(
                name: "TaskDescription",
                table: "WorkLogs",
                newName: "Description");
        }
    }
}
