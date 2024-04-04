using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gakko.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "appointmentstatus",
                columns: table => new
                {
                    idappointmentstatus = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("appointmentstatus_pk", x => x.idappointmentstatus);
                });

            migrationBuilder.CreateTable(
                name: "documenttype",
                columns: table => new
                {
                    iddocumenttype = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("documenttype_pk", x => x.iddocumenttype);
                });

            migrationBuilder.CreateTable(
                name: "nationality",
                columns: table => new
                {
                    idnationality = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("nationality_pk", x => x.idnationality);
                });

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    idstatus = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("status_pk", x => x.idstatus);
                });

            migrationBuilder.CreateTable(
                name: "studylevel",
                columns: table => new
                {
                    idstudylevel = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("studylevel_pk", x => x.idstudylevel);
                });

            migrationBuilder.CreateTable(
                name: "studymode",
                columns: table => new
                {
                    idstudymode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("studymode_pk", x => x.idstudymode);
                });

            migrationBuilder.CreateTable(
                name: "studyprogramme",
                columns: table => new
                {
                    idstudyprogramme = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    idstudylevel = table.Column<int>(type: "integer", nullable: false),
                    recruitmentstart = table.Column<DateOnly>(type: "date", nullable: false),
                    recruitmentend = table.Column<DateOnly>(type: "date", nullable: false),
                    idstudymode = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("studyprogramme_pk", x => x.idstudyprogramme);
                    table.ForeignKey(
                        name: "studyprogrammer_studycycle",
                        column: x => x.idstudylevel,
                        principalTable: "studylevel",
                        principalColumn: "idstudylevel");
                    table.ForeignKey(
                        name: "studyprogrammer_studymode",
                        column: x => x.idstudymode,
                        principalTable: "studymode",
                        principalColumn: "idstudymode");
                });

            migrationBuilder.CreateTable(
                name: "requiredenrollmentdocument",
                columns: table => new
                {
                    idstudyprogramme = table.Column<int>(type: "integer", nullable: false),
                    iddocumenttype = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("requiredenrollmentdocument_pk", x => new { x.idstudyprogramme, x.iddocumenttype });
                    table.ForeignKey(
                        name: "requiredenrollmentdocument_documenttype",
                        column: x => x.iddocumenttype,
                        principalTable: "documenttype",
                        principalColumn: "iddocumenttype");
                    table.ForeignKey(
                        name: "requiredenrollmentdocument_studyprogrammer",
                        column: x => x.idstudyprogramme,
                        principalTable: "studyprogramme",
                        principalColumn: "idstudyprogramme");
                });

            migrationBuilder.CreateTable(
                name: "student",
                columns: table => new
                {
                    idcandidate = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstname = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    lastname = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    phonenumber = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    emailaddress = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    homeaddress = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    indexnumber = table.Column<int>(type: "integer", nullable: true),
                    gender = table.Column<int>(type: "integer", nullable: false),
                    peselnumber = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    passportnumber = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: true),
                    dateofbirth = table.Column<DateOnly>(type: "date", nullable: false),
                    idnationality = table.Column<int>(type: "integer", nullable: false),
                    idstudyprogramme = table.Column<int>(type: "integer", nullable: false),
                    idstatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("student_pk", x => x.idcandidate);
                    table.ForeignKey(
                        name: "candidate_nationality",
                        column: x => x.idnationality,
                        principalTable: "nationality",
                        principalColumn: "idnationality");
                    table.ForeignKey(
                        name: "candidate_studyprogrammer",
                        column: x => x.idstudyprogramme,
                        principalTable: "studyprogramme",
                        principalColumn: "idstudyprogramme");
                    table.ForeignKey(
                        name: "student_status",
                        column: x => x.idstatus,
                        principalTable: "status",
                        principalColumn: "idstatus");
                });

            migrationBuilder.CreateTable(
                name: "appointment",
                columns: table => new
                {
                    idappointment = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    idappointmentstatus = table.Column<int>(type: "integer", nullable: false),
                    idcandidate = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("appointment_pk", x => x.idappointment);
                    table.ForeignKey(
                        name: "appointment_appointmentstatus",
                        column: x => x.idappointmentstatus,
                        principalTable: "appointmentstatus",
                        principalColumn: "idappointmentstatus");
                    table.ForeignKey(
                        name: "appointment_candidate",
                        column: x => x.idcandidate,
                        principalTable: "student",
                        principalColumn: "idcandidate");
                });

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
                table: "nationality",
                columns: new[] { "idnationality", "name" },
                values: new object[,]
                {
                    { 1, "American" },
                    { 2, "Canadian" },
                    { 3, "British" },
                    { 4, "Australian" },
                    { 5, "French" },
                    { 6, "German" },
                    { 7, "Polish" }
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
                table: "studyprogramme",
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

            migrationBuilder.CreateIndex(
                name: "IX_appointment_idappointmentstatus",
                table: "appointment",
                column: "idappointmentstatus");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_idcandidate",
                table: "appointment",
                column: "idcandidate");

            migrationBuilder.CreateIndex(
                name: "IX_candidatesdocument_IdDocumentType",
                table: "candidatesdocument",
                column: "IdDocumentType");

            migrationBuilder.CreateIndex(
                name: "IX_requiredenrollmentdocument_iddocumenttype",
                table: "requiredenrollmentdocument",
                column: "iddocumenttype");

            migrationBuilder.CreateIndex(
                name: "IX_student_idnationality",
                table: "student",
                column: "idnationality");

            migrationBuilder.CreateIndex(
                name: "IX_student_idstatus",
                table: "student",
                column: "idstatus");

            migrationBuilder.CreateIndex(
                name: "IX_student_idstudyprogramme",
                table: "student",
                column: "idstudyprogramme");

            migrationBuilder.CreateIndex(
                name: "IX_studyprogramme_idstudylevel",
                table: "studyprogramme",
                column: "idstudylevel");

            migrationBuilder.CreateIndex(
                name: "IX_studyprogramme_idstudymode",
                table: "studyprogramme",
                column: "idstudymode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointment");

            migrationBuilder.DropTable(
                name: "candidatesdocument");

            migrationBuilder.DropTable(
                name: "requiredenrollmentdocument");

            migrationBuilder.DropTable(
                name: "appointmentstatus");

            migrationBuilder.DropTable(
                name: "student");

            migrationBuilder.DropTable(
                name: "documenttype");

            migrationBuilder.DropTable(
                name: "nationality");

            migrationBuilder.DropTable(
                name: "studyprogramme");

            migrationBuilder.DropTable(
                name: "status");

            migrationBuilder.DropTable(
                name: "studylevel");

            migrationBuilder.DropTable(
                name: "studymode");
        }
    }
}
