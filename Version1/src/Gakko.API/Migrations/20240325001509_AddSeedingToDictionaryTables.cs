using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gakko.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedingToDictionaryTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "documenttype",
                columns: new[] { "iddocumenttype", "name" },
                values: new object[,]
                {
                    { 1, "High school diploma" },
                    { 2, "Bachelor's degree" },
                    { 3, "Master's degree" },
                    { 4, "Doctoral degree" },
                    { 5, "English language certificate" },
                    { 6, "Passport" },
                    { 7, "Photo" }
                });

            migrationBuilder.InsertData(
                table: "status",
                columns: new[] { "idstatus", "name" },
                values: new object[,]
                {
                    { 1, "Candidate - registered" },
                    { 2, "Candidate - waiting for documents" },
                    { 3, "Candidate - waiting for signing contract" },
                    { 4, "Candidate - waiting for payment" },
                    { 5, "Student" },
                    { 6, "Graduate" },
                    { 7, "Student on leave" }
                });

            migrationBuilder.InsertData(
                table: "studylevel",
                columns: new[] { "idstudylevel", "name" },
                values: new object[,]
                {
                    { 1, "Bachelor" },
                    { 2, "Master" },
                    { 3, "Doctoral" }
                });

            migrationBuilder.InsertData(
                table: "studymode",
                columns: new[] { "idstudymode", "name" },
                values: new object[,]
                {
                    { 1, "Full-time" },
                    { 2, "Part-time" }
                });

            migrationBuilder.InsertData(
                table: "studyprogrammer",
                columns: new[] { "idstudyprogramme", "idstudylevel", "idstudymode", "name", "recruitmentend", "recruitmentstart" },
                values: new object[,]
                {
                    { 1, 1, 1, "Computer Science", new DateOnly(2022, 9, 30), new DateOnly(2022, 1, 1) },
                    { 2, 1, 1, "Information Technology", new DateOnly(2022, 9, 30), new DateOnly(2022, 1, 1) },
                    { 3, 1, 1, "Software Engineering", new DateOnly(2022, 9, 30), new DateOnly(2022, 1, 1) },
                    { 4, 2, 1, "Computer Science", new DateOnly(2022, 9, 30), new DateOnly(2022, 1, 1) },
                    { 5, 2, 1, "Information Technology", new DateOnly(2022, 9, 30), new DateOnly(2022, 1, 1) },
                    { 6, 2, 1, "Software Engineering", new DateOnly(2022, 9, 30), new DateOnly(2022, 1, 1) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "documenttype",
                keyColumn: "iddocumenttype",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "documenttype",
                keyColumn: "iddocumenttype",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "documenttype",
                keyColumn: "iddocumenttype",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "documenttype",
                keyColumn: "iddocumenttype",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "documenttype",
                keyColumn: "iddocumenttype",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "documenttype",
                keyColumn: "iddocumenttype",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "documenttype",
                keyColumn: "iddocumenttype",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "status",
                keyColumn: "idstatus",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "status",
                keyColumn: "idstatus",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "status",
                keyColumn: "idstatus",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "status",
                keyColumn: "idstatus",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "status",
                keyColumn: "idstatus",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "status",
                keyColumn: "idstatus",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "status",
                keyColumn: "idstatus",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "studylevel",
                keyColumn: "idstudylevel",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "studymode",
                keyColumn: "idstudymode",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "studyprogrammer",
                keyColumn: "idstudyprogramme",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "studyprogrammer",
                keyColumn: "idstudyprogramme",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "studyprogrammer",
                keyColumn: "idstudyprogramme",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "studyprogrammer",
                keyColumn: "idstudyprogramme",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "studyprogrammer",
                keyColumn: "idstudyprogramme",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "studyprogrammer",
                keyColumn: "idstudyprogramme",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "studylevel",
                keyColumn: "idstudylevel",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "studylevel",
                keyColumn: "idstudylevel",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "studymode",
                keyColumn: "idstudymode",
                keyValue: 1);
        }
    }
}
