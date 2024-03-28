﻿// <auto-generated />
using System;
using Gakko.API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gakko.API.Migrations
{
    [DbContext(typeof(GakkoContext))]
    [Migration("20240328151026_ChangedLengthOfIndexNumberInStudentEntity")]
    partial class ChangedLengthOfIndexNumberInStudentEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.2.24128.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Gakko.API.Models.Appointment", b =>
                {
                    b.Property<int>("IdAppointment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idappointment");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdAppointment"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date")
                        .HasColumnName("date");

                    b.Property<int>("IdAppointmentStatus")
                        .HasColumnType("integer")
                        .HasColumnName("idappointmentstatus");

                    b.Property<int>("IdCandidate")
                        .HasColumnType("integer")
                        .HasColumnName("idcandidate");

                    b.HasKey("IdAppointment")
                        .HasName("appointment_pk");

                    b.HasIndex("IdAppointmentStatus");

                    b.HasIndex("IdCandidate");

                    b.ToTable("appointment", (string)null);
                });

            modelBuilder.Entity("Gakko.API.Models.AppointmentStatus", b =>
                {
                    b.Property<int>("IdAppointmentStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idappointmentstatus");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdAppointmentStatus"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("IdAppointmentStatus")
                        .HasName("appointmentstatus_pk");

                    b.ToTable("appointmentstatus", (string)null);

                    b.HasData(
                        new
                        {
                            IdAppointmentStatus = 1,
                            Name = "Scheduled"
                        },
                        new
                        {
                            IdAppointmentStatus = 2,
                            Name = "Cancelled"
                        },
                        new
                        {
                            IdAppointmentStatus = 3,
                            Name = "Done"
                        });
                });

            modelBuilder.Entity("Gakko.API.Models.CandidatesDocument", b =>
                {
                    b.Property<int>("IdCandidate")
                        .HasColumnType("integer");

                    b.Property<int>("IdDocumentType")
                        .HasColumnType("integer");

                    b.Property<DateOnly?>("ConfirmedAt")
                        .HasColumnType("date")
                        .HasColumnName("confirmedat");

                    b.HasKey("IdCandidate", "IdDocumentType")
                        .HasName("candidatesdocument_pk");

                    b.HasIndex("IdDocumentType");

                    b.ToTable("candidatesdocument", (string)null);
                });

            modelBuilder.Entity("Gakko.API.Models.DocumentType", b =>
                {
                    b.Property<int>("IdDocumentType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("iddocumenttype");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdDocumentType"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("IdDocumentType")
                        .HasName("documenttype_pk");

                    b.ToTable("documenttype", (string)null);

                    b.HasData(
                        new
                        {
                            IdDocumentType = 1,
                            Name = "High school diploma"
                        },
                        new
                        {
                            IdDocumentType = 2,
                            Name = "Bachelor's degree"
                        },
                        new
                        {
                            IdDocumentType = 3,
                            Name = "Master's degree"
                        },
                        new
                        {
                            IdDocumentType = 4,
                            Name = "Doctoral degree"
                        },
                        new
                        {
                            IdDocumentType = 5,
                            Name = "English language certificate"
                        },
                        new
                        {
                            IdDocumentType = 6,
                            Name = "Passport"
                        },
                        new
                        {
                            IdDocumentType = 7,
                            Name = "Photo"
                        });
                });

            modelBuilder.Entity("Gakko.API.Models.Nationality", b =>
                {
                    b.Property<int>("IdNationality")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idnationality");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdNationality"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("IdNationality")
                        .HasName("nationality_pk");

                    b.ToTable("nationality", (string)null);

                    b.HasData(
                        new
                        {
                            IdNationality = 1,
                            Name = "American"
                        },
                        new
                        {
                            IdNationality = 2,
                            Name = "Canadian"
                        },
                        new
                        {
                            IdNationality = 3,
                            Name = "British"
                        },
                        new
                        {
                            IdNationality = 4,
                            Name = "Australian"
                        },
                        new
                        {
                            IdNationality = 5,
                            Name = "French"
                        },
                        new
                        {
                            IdNationality = 6,
                            Name = "German"
                        },
                        new
                        {
                            IdNationality = 7,
                            Name = "Polish"
                        });
                });

            modelBuilder.Entity("Gakko.API.Models.Status", b =>
                {
                    b.Property<int>("IdStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idstatus");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdStatus"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name");

                    b.HasKey("IdStatus")
                        .HasName("status_pk");

                    b.ToTable("status", (string)null);

                    b.HasData(
                        new
                        {
                            IdStatus = 1,
                            Name = "Candidate - registered"
                        },
                        new
                        {
                            IdStatus = 2,
                            Name = "Candidate - waiting for documents"
                        },
                        new
                        {
                            IdStatus = 3,
                            Name = "Candidate - waiting for signing contract"
                        },
                        new
                        {
                            IdStatus = 4,
                            Name = "Candidate - waiting for payment"
                        },
                        new
                        {
                            IdStatus = 5,
                            Name = "Student"
                        },
                        new
                        {
                            IdStatus = 6,
                            Name = "Graduate"
                        },
                        new
                        {
                            IdStatus = 7,
                            Name = "Student on leave"
                        });
                });

            modelBuilder.Entity("Gakko.API.Models.Student", b =>
                {
                    b.Property<int>("IdCandidate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idcandidate");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdCandidate"));

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnName("dateofbirth");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("emailaddress");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("firstname");

                    b.Property<int>("Gender")
                        .HasColumnType("integer")
                        .HasColumnName("gender");

                    b.Property<string>("HomeAddress")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("homeaddress");

                    b.Property<int>("IdNationality")
                        .HasColumnType("integer")
                        .HasColumnName("idnationality");

                    b.Property<int>("IdStatus")
                        .HasColumnType("integer")
                        .HasColumnName("idstatus");

                    b.Property<int>("IdStudyProgramme")
                        .HasColumnType("integer")
                        .HasColumnName("idstudyprogramme");

                    b.Property<string>("IndexNumber")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("indexnumber");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("lastname");

                    b.Property<string>("PassportNumber")
                        .HasMaxLength(9)
                        .HasColumnType("character varying(9)")
                        .HasColumnName("passportnumber");

                    b.Property<string>("PeselNumber")
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)")
                        .HasColumnName("peselnumber");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("phonenumber");

                    b.HasKey("IdCandidate")
                        .HasName("student_pk");

                    b.HasIndex("IdNationality");

                    b.HasIndex("IdStatus");

                    b.HasIndex("IdStudyProgramme");

                    b.ToTable("student", (string)null);
                });

            modelBuilder.Entity("Gakko.API.Models.StudyLevel", b =>
                {
                    b.Property<int>("IdStudyLevel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idstudylevel");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdStudyLevel"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("IdStudyLevel")
                        .HasName("studylevel_pk");

                    b.ToTable("studylevel", (string)null);

                    b.HasData(
                        new
                        {
                            IdStudyLevel = 1,
                            Name = "Bachelor"
                        },
                        new
                        {
                            IdStudyLevel = 2,
                            Name = "Master"
                        },
                        new
                        {
                            IdStudyLevel = 3,
                            Name = "Doctoral"
                        });
                });

            modelBuilder.Entity("Gakko.API.Models.StudyMode", b =>
                {
                    b.Property<int>("IdStudyMode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idstudymode");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdStudyMode"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("IdStudyMode")
                        .HasName("studymode_pk");

                    b.ToTable("studymode", (string)null);

                    b.HasData(
                        new
                        {
                            IdStudyMode = 1,
                            Name = "Full-time"
                        },
                        new
                        {
                            IdStudyMode = 2,
                            Name = "Part-time"
                        });
                });

            modelBuilder.Entity("Gakko.API.Models.StudyProgramme", b =>
                {
                    b.Property<int>("IdStudyProgramme")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("idstudyprogramme");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdStudyProgramme"));

                    b.Property<int>("IdStudyLevel")
                        .HasColumnType("integer")
                        .HasColumnName("idstudylevel");

                    b.Property<int>("IdStudyMode")
                        .HasColumnType("integer")
                        .HasColumnName("idstudymode");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<DateOnly>("RecruitmentEnd")
                        .HasColumnType("date")
                        .HasColumnName("recruitmentend");

                    b.Property<DateOnly>("RecruitmentStart")
                        .HasColumnType("date")
                        .HasColumnName("recruitmentstart");

                    b.HasKey("IdStudyProgramme")
                        .HasName("studyprogrammer_pk");

                    b.HasIndex("IdStudyLevel");

                    b.HasIndex("IdStudyMode");

                    b.ToTable("studyprogrammer", (string)null);

                    b.HasData(
                        new
                        {
                            IdStudyProgramme = 1,
                            IdStudyLevel = 1,
                            IdStudyMode = 1,
                            Name = "Computer Science",
                            RecruitmentEnd = new DateOnly(2022, 9, 30),
                            RecruitmentStart = new DateOnly(2022, 1, 1)
                        },
                        new
                        {
                            IdStudyProgramme = 2,
                            IdStudyLevel = 1,
                            IdStudyMode = 1,
                            Name = "Information Technology",
                            RecruitmentEnd = new DateOnly(2022, 9, 30),
                            RecruitmentStart = new DateOnly(2022, 1, 1)
                        },
                        new
                        {
                            IdStudyProgramme = 3,
                            IdStudyLevel = 1,
                            IdStudyMode = 1,
                            Name = "Software Engineering",
                            RecruitmentEnd = new DateOnly(2022, 9, 30),
                            RecruitmentStart = new DateOnly(2022, 1, 1)
                        },
                        new
                        {
                            IdStudyProgramme = 4,
                            IdStudyLevel = 2,
                            IdStudyMode = 1,
                            Name = "Computer Science",
                            RecruitmentEnd = new DateOnly(2022, 9, 30),
                            RecruitmentStart = new DateOnly(2022, 1, 1)
                        },
                        new
                        {
                            IdStudyProgramme = 5,
                            IdStudyLevel = 2,
                            IdStudyMode = 1,
                            Name = "Information Technology",
                            RecruitmentEnd = new DateOnly(2022, 9, 30),
                            RecruitmentStart = new DateOnly(2022, 1, 1)
                        },
                        new
                        {
                            IdStudyProgramme = 6,
                            IdStudyLevel = 2,
                            IdStudyMode = 1,
                            Name = "Software Engineering",
                            RecruitmentEnd = new DateOnly(2022, 9, 30),
                            RecruitmentStart = new DateOnly(2022, 1, 1)
                        });
                });

            modelBuilder.Entity("Requiredenrollmentdocument", b =>
                {
                    b.Property<int>("Idstudyprogramme")
                        .HasColumnType("integer")
                        .HasColumnName("idstudyprogramme");

                    b.Property<int>("Iddocumenttype")
                        .HasColumnType("integer")
                        .HasColumnName("iddocumenttype");

                    b.HasKey("Idstudyprogramme", "Iddocumenttype")
                        .HasName("requiredenrollmentdocument_pk");

                    b.HasIndex("Iddocumenttype");

                    b.ToTable("requiredenrollmentdocument", (string)null);
                });

            modelBuilder.Entity("Gakko.API.Models.Appointment", b =>
                {
                    b.HasOne("Gakko.API.Models.AppointmentStatus", "AppointmentStatus")
                        .WithMany("Appointments")
                        .HasForeignKey("IdAppointmentStatus")
                        .IsRequired()
                        .HasConstraintName("appointment_appointmentstatus");

                    b.HasOne("Gakko.API.Models.Student", "CandidateNavigation")
                        .WithMany("Appointments")
                        .HasForeignKey("IdCandidate")
                        .IsRequired()
                        .HasConstraintName("appointment_candidate");

                    b.Navigation("AppointmentStatus");

                    b.Navigation("CandidateNavigation");
                });

            modelBuilder.Entity("Gakko.API.Models.CandidatesDocument", b =>
                {
                    b.HasOne("Gakko.API.Models.Student", "Candidate")
                        .WithMany("CandidatesDocuments")
                        .HasForeignKey("IdCandidate")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gakko.API.Models.DocumentType", "DocumentType")
                        .WithMany("CandidatesDocuments")
                        .HasForeignKey("IdDocumentType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidate");

                    b.Navigation("DocumentType");
                });

            modelBuilder.Entity("Gakko.API.Models.Student", b =>
                {
                    b.HasOne("Gakko.API.Models.Nationality", "Nationality")
                        .WithMany("Students")
                        .HasForeignKey("IdNationality")
                        .IsRequired()
                        .HasConstraintName("candidate_nationality");

                    b.HasOne("Gakko.API.Models.Status", "Status")
                        .WithMany("Students")
                        .HasForeignKey("IdStatus")
                        .IsRequired()
                        .HasConstraintName("student_status");

                    b.HasOne("Gakko.API.Models.StudyProgramme", "StudyProgramme")
                        .WithMany("Students")
                        .HasForeignKey("IdStudyProgramme")
                        .IsRequired()
                        .HasConstraintName("candidate_studyprogrammer");

                    b.Navigation("Nationality");

                    b.Navigation("Status");

                    b.Navigation("StudyProgramme");
                });

            modelBuilder.Entity("Gakko.API.Models.StudyProgramme", b =>
                {
                    b.HasOne("Gakko.API.Models.StudyLevel", "StudyLevel")
                        .WithMany("StudyProgrammes")
                        .HasForeignKey("IdStudyLevel")
                        .IsRequired()
                        .HasConstraintName("studyprogrammer_studycycle");

                    b.HasOne("Gakko.API.Models.StudyMode", "StudyMode")
                        .WithMany("StudyProgrammes")
                        .HasForeignKey("IdStudyMode")
                        .IsRequired()
                        .HasConstraintName("studyprogrammer_studymode");

                    b.Navigation("StudyLevel");

                    b.Navigation("StudyMode");
                });

            modelBuilder.Entity("Requiredenrollmentdocument", b =>
                {
                    b.HasOne("Gakko.API.Models.DocumentType", null)
                        .WithMany()
                        .HasForeignKey("Iddocumenttype")
                        .IsRequired()
                        .HasConstraintName("requiredenrollmentdocument_documenttype");

                    b.HasOne("Gakko.API.Models.StudyProgramme", null)
                        .WithMany()
                        .HasForeignKey("Idstudyprogramme")
                        .IsRequired()
                        .HasConstraintName("requiredenrollmentdocument_studyprogrammer");
                });

            modelBuilder.Entity("Gakko.API.Models.AppointmentStatus", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("Gakko.API.Models.DocumentType", b =>
                {
                    b.Navigation("CandidatesDocuments");
                });

            modelBuilder.Entity("Gakko.API.Models.Nationality", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Gakko.API.Models.Status", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Gakko.API.Models.Student", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("CandidatesDocuments");
                });

            modelBuilder.Entity("Gakko.API.Models.StudyLevel", b =>
                {
                    b.Navigation("StudyProgrammes");
                });

            modelBuilder.Entity("Gakko.API.Models.StudyMode", b =>
                {
                    b.Navigation("StudyProgrammes");
                });

            modelBuilder.Entity("Gakko.API.Models.StudyProgramme", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
