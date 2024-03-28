using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gakko.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedIndexNumberToStudentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IndexNumber",
                table: "student",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IndexNumber",
                table: "student");
        }
    }
}
