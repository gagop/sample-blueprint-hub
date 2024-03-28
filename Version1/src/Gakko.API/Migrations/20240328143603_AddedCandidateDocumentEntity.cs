using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gakko.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedCandidateDocumentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "candidatesdocument",
                columns: table => new
                {
                    IdCandidate = table.Column<int>(type: "integer", nullable: false),
                    IdDocumentType = table.Column<int>(type: "integer", nullable: false),
                    confirmedat = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("candidatesdocument_pk", x => new { x.IdCandidate, x.IdDocumentType });
                    table.ForeignKey(
                        name: "FK_candidatesdocument_documenttype_IdDocumentType",
                        column: x => x.IdDocumentType,
                        principalTable: "documenttype",
                        principalColumn: "iddocumenttype",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_candidatesdocument_student_IdCandidate",
                        column: x => x.IdCandidate,
                        principalTable: "student",
                        principalColumn: "idcandidate",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "appointmentstatus",
                columns: new[] { "idappointmentstatus", "name" },
                values: new object[,]
                {
                    { 1, "Scheduled" },
                    { 2, "Cancelled" },
                    { 3, "Done" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_candidatesdocument_IdDocumentType",
                table: "candidatesdocument",
                column: "IdDocumentType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "candidatesdocument");

            migrationBuilder.DeleteData(
                table: "appointmentstatus",
                keyColumn: "idappointmentstatus",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "appointmentstatus",
                keyColumn: "idappointmentstatus",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "appointmentstatus",
                keyColumn: "idappointmentstatus",
                keyValue: 3);
        }
    }
}
