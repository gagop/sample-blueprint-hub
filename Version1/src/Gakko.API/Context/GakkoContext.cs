using Gakko.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Gakko.API.Context;

public partial class GakkoContext : DbContext
{
    public GakkoContext()
    {
    }

    public GakkoContext(DbContextOptions<GakkoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }
    public virtual DbSet<AppointmentStatus> AppointmentStatuses { get; set; }

    public virtual DbSet<DocumentType> DocumentTypes { get; set; }
    public virtual DbSet<CandidatesDocument> CandidatesDocument { get; set; }
    public virtual DbSet<Nationality> Nationalities { get; set; }
    public virtual DbSet<Status> Statuses { get; set; }
    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudyLevel> StudyLevels { get; set; }

    public virtual DbSet<StudyMode> StudyModes { get; set; }

    public virtual DbSet<StudyProgramme> StudyProgrammes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SeedDatabase(modelBuilder);

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.IdAppointment).HasName("appointment_pk");

            entity.ToTable("appointment");

            entity.Property(e => e.IdAppointment)
                .ValueGeneratedOnAdd()
                .HasColumnName("idappointment");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.IdAppointmentStatus).HasColumnName("idappointmentstatus");
            entity.Property(e => e.IdCandidate).HasColumnName("idcandidate");

            entity.HasOne(d => d.AppointmentStatus).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.IdAppointmentStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("appointment_appointmentstatus");

            entity.HasOne(d => d.CandidateNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.IdCandidate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("appointment_candidate");
        });

        modelBuilder.Entity<AppointmentStatus>(entity =>
        {
            entity.HasKey(e => e.IdAppointmentStatus).HasName("appointmentstatus_pk");

            entity.ToTable("appointmentstatus");

            entity.Property(e => e.IdAppointmentStatus)
                .ValueGeneratedOnAdd()
                .HasColumnName("idappointmentstatus");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<DocumentType>(entity =>
        {
            entity.HasKey(e => e.IdDocumentType).HasName("documenttype_pk");

            entity.ToTable("documenttype");

            entity.Property(e => e.IdDocumentType)
                .ValueGeneratedOnAdd()
                .HasColumnName("iddocumenttype");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Nationality>(entity =>
        {
            entity.HasKey(e => e.IdNationality).HasName("nationality_pk");

            entity.ToTable("nationality");

            entity.Property(e => e.IdNationality)
                .ValueGeneratedOnAdd()
                .HasColumnName("idnationality");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("status_pk");

            entity.ToTable("status");

            entity.Property(e => e.IdStatus)
                .ValueGeneratedOnAdd()
                .HasColumnName("idstatus");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.IdCandidate).HasName("student_pk");

            entity.ToTable("student");

            entity.Property(e => e.IdCandidate)
                .ValueGeneratedOnAdd()
                .HasColumnName("idcandidate");
            entity.Property(e => e.DateOfBirth).HasColumnName("dateofbirth");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(200)
                .HasColumnName("emailaddress");
            entity.Property(e => e.FirstName)
                .HasMaxLength(200)
                .HasColumnName("firstname");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.HomeAddress)
                .HasMaxLength(255)
                .HasColumnName("homeaddress");
            entity.Property(e => e.IndexNumber).HasColumnName("indexnumber");
            entity.Property(e => e.IdNationality).HasColumnName("idnationality");
            entity.Property(e => e.IdStatus).HasColumnName("idstatus");
            entity.Property(e => e.IdStudyProgramme).HasColumnName("idstudyprogramme");
            entity.Property(e => e.LastName)
                .HasMaxLength(200)
                .HasColumnName("lastname");
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(9)
                .HasColumnName("passportnumber");
            entity.Property(e => e.PeselNumber)
                .HasMaxLength(11)
                .HasColumnName("peselnumber");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(200)
                .HasColumnName("phonenumber");

            entity.HasOne(d => d.Nationality).WithMany(p => p.Students)
                .HasForeignKey(d => d.IdNationality)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("candidate_nationality");

            entity.HasOne(d => d.Status).WithMany(p => p.Students)
                .HasForeignKey(d => d.IdStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_status");

            entity.HasOne(d => d.StudyProgramme).WithMany(p => p.Students)
                .HasForeignKey(d => d.IdStudyProgramme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("candidate_studyprogrammer");
        });

        modelBuilder.Entity<StudyLevel>(entity =>
        {
            entity.HasKey(e => e.IdStudyLevel).HasName("studylevel_pk");

            entity.ToTable("studylevel");

            entity.Property(e => e.IdStudyLevel)
                .ValueGeneratedOnAdd()
                .HasColumnName("idstudylevel");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<StudyMode>(entity =>
        {
            entity.HasKey(e => e.IdStudyMode).HasName("studymode_pk");

            entity.ToTable("studymode");

            entity.Property(e => e.IdStudyMode)
                .ValueGeneratedOnAdd()
                .HasColumnName("idstudymode");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<StudyProgramme>(entity =>
        {
            entity.HasKey(e => e.IdStudyProgramme).HasName("studyprogramme_pk");

            entity.ToTable("studyprogramme");

            entity.Property(e => e.IdStudyProgramme)
                .ValueGeneratedOnAdd()
                .HasColumnName("idstudyprogramme");
            entity.Property(e => e.IdStudyLevel).HasColumnName("idstudylevel");
            entity.Property(e => e.IdStudyMode).HasColumnName("idstudymode");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.RecruitmentEnd).HasColumnName("recruitmentend");
            entity.Property(e => e.RecruitmentStart).HasColumnName("recruitmentstart");

            entity.HasOne(d => d.StudyLevel).WithMany(p => p.StudyProgrammes)
                .HasForeignKey(d => d.IdStudyLevel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("studyprogrammer_studycycle");

            entity.HasOne(d => d.StudyMode).WithMany(p => p.StudyProgrammes)
                .HasForeignKey(d => d.IdStudyMode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("studyprogrammer_studymode");

            entity.HasMany(d => d.DocumentTypes).WithMany(p => p.StudyProgrammes)
                .UsingEntity<Dictionary<string, object>>(
                    "Requiredenrollmentdocument",
                    r => r.HasOne<DocumentType>().WithMany()
                        .HasForeignKey("Iddocumenttype")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("requiredenrollmentdocument_documenttype"),
                    l => l.HasOne<StudyProgramme>().WithMany()
                        .HasForeignKey("Idstudyprogramme")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("requiredenrollmentdocument_studyprogrammer"),
                    j =>
                    {
                        j.HasKey("Idstudyprogramme", "Iddocumenttype").HasName("requiredenrollmentdocument_pk");
                        j.ToTable("requiredenrollmentdocument");
                        j.IndexerProperty<int>("Idstudyprogramme").HasColumnName("idstudyprogramme");
                        j.IndexerProperty<int>("Iddocumenttype").HasColumnName("iddocumenttype");
                    });
        });

        modelBuilder.Entity<CandidatesDocument>(entity =>
        {
            entity.HasKey(e => new
            {
                e.IdCandidate,
                e.IdDocumentType
            }).HasName("candidatesdocument_pk");

            entity.ToTable("candidatesdocument");

            entity.Property(e => e.ConfirmedAt).HasColumnName("confirmedat");
            entity.HasOne(e => e.Candidate)
                .WithMany(e => e.CandidatesDocuments);
            entity.HasOne(e => e.DocumentType)
                .WithMany(e => e.CandidatesDocuments);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    private static void SeedDatabase(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Nationality>().HasData(
            new Nationality { IdNationality = 1, Name = "American" },
            new Nationality { IdNationality = 2, Name = "Canadian" },
            new Nationality { IdNationality = 3, Name = "British" },
            new Nationality { IdNationality = 4, Name = "Australian" },
            new Nationality { IdNationality = 5, Name = "French" },
            new Nationality { IdNationality = 6, Name = "German" },
            new Nationality { IdNationality = 7, Name = "Polish" }
        );

        modelBuilder.Entity<Status>().HasData(
            new Status { IdStatus = 1, Name = "Candidate - registered" },
            new Status { IdStatus = 2, Name = "Candidate - waiting for documents" },
            new Status { IdStatus = 3, Name = "Candidate - waiting for signing contract" },
            new Status { IdStatus = 4, Name = "Candidate - waiting for payment" },
            new Status { IdStatus = 5, Name = "Student" },
            new Status { IdStatus = 6, Name = "Graduate" },
            new Status { IdStatus = 7, Name = "Student on leave" },
            new Status { IdStatus = 8, Name = "Candidate - cancelled" }
        );

        modelBuilder.Entity<StudyMode>().HasData(
            new StudyMode { IdStudyMode = 1, Name = "Full-time" },
            new StudyMode { IdStudyMode = 2, Name = "Part-time" }
        );

        modelBuilder.Entity<StudyLevel>().HasData(
            new StudyLevel { IdStudyLevel = 1, Name = "Bachelor" },
            new StudyLevel { IdStudyLevel = 2, Name = "Master" },
            new StudyLevel { IdStudyLevel = 3, Name = "Doctoral" }
        );

        var sp = new StudyProgramme
        {
            IdStudyProgramme = 1, IdStudyLevel = 1, IdStudyMode = 1, Name = "Computer Science",
            RecruitmentStart = new DateOnly(2022, 1, 1), RecruitmentEnd = new DateOnly(2022, 9, 30)
        };

        modelBuilder.Entity<StudyProgramme>().HasData(
            sp,
            new StudyProgramme
            {
                IdStudyProgramme = 2, IdStudyLevel = 1, IdStudyMode = 1, Name = "Information Technology",
                RecruitmentStart = new DateOnly(2022, 1, 1), RecruitmentEnd = new DateOnly(2022, 9, 30)
            },
            new StudyProgramme
            {
                IdStudyProgramme = 3, IdStudyLevel = 1, IdStudyMode = 1, Name = "Software Engineering",
                RecruitmentStart = new DateOnly(2022, 1, 1), RecruitmentEnd = new DateOnly(2022, 9, 30)
            },
            new StudyProgramme
            {
                IdStudyProgramme = 4, IdStudyLevel = 2, IdStudyMode = 1, Name = "Computer Science",
                RecruitmentStart = new DateOnly(2022, 1, 1), RecruitmentEnd = new DateOnly(2022, 9, 30)
            },
            new StudyProgramme
            {
                IdStudyProgramme = 5, IdStudyLevel = 2, IdStudyMode = 1, Name = "Information Technology",
                RecruitmentStart = new DateOnly(2022, 1, 1), RecruitmentEnd = new DateOnly(2022, 9, 30)
            },
            new StudyProgramme
            {
                IdStudyProgramme = 6, IdStudyLevel = 2, IdStudyMode = 1, Name = "Software Engineering",
                RecruitmentStart = new DateOnly(2022, 1, 1), RecruitmentEnd = new DateOnly(2022, 9, 30)
            }
        );

        modelBuilder.Entity<DocumentType>().HasData(
            new DocumentType
            {
                IdDocumentType = 1, Name = "High school diploma"
            },
            new DocumentType { IdDocumentType = 2, Name = "Bachelor's degree" },
            new DocumentType { IdDocumentType = 3, Name = "Master's degree" },
            new DocumentType { IdDocumentType = 4, Name = "Doctoral degree" },
            new DocumentType { IdDocumentType = 5, Name = "English language certificate" },
            new DocumentType { IdDocumentType = 6, Name = "Passport" },
            new DocumentType { IdDocumentType = 7, Name = "Photo" }
        );

        modelBuilder.Entity<AppointmentStatus>().HasData(
            new AppointmentStatus { IdAppointmentStatus = 1, Name = "Scheduled" },
            new AppointmentStatus { IdAppointmentStatus = 2, Name = "Cancelled" },
            new AppointmentStatus { IdAppointmentStatus = 3, Name = "Done" }
        );

        modelBuilder.Entity("Requiredenrollmentdocument").HasData(
            new { Idstudyprogramme = 1, Iddocumenttype = 1 },
            new { Idstudyprogramme = 1, Iddocumenttype = 5 },
            new { Idstudyprogramme = 2, Iddocumenttype = 2 },
            new { Idstudyprogramme = 3, Iddocumenttype = 3 }
            // Add as many links as needed
        );
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}