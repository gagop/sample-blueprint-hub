using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gakko.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangedLengthOfIndexNumberInStudentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IndexNumber",
                table: "student",
                newName: "indexnumber");

            migrationBuilder.AlterColumn<string>(
                name: "indexnumber",
                table: "student",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "indexnumber",
                table: "student",
                newName: "IndexNumber");

            migrationBuilder.AlterColumn<string>(
                name: "IndexNumber",
                table: "student",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
