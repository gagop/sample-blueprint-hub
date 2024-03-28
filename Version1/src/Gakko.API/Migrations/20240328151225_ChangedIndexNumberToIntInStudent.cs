using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gakko.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangedIndexNumberToIntInStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "indexnumber",
                table: "student",
                type: "integer",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "indexnumber",
                table: "student",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
