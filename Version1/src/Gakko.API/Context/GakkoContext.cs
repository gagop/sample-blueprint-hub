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

    public virtual DbSet<Appointmentstatus> Appointmentstatuses { get; set; }

    public virtual DbSet<DocumentType> Documenttypes { get; set; }

    public virtual DbSet<Nationality> Nationalities { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudyLevel> Studylevels { get; set; }

    public virtual DbSet<StudyMode> StudyModes { get; set; }

    public virtual DbSet<StudyProgramme> StudyProgrammes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.IdAppointment).HasName("appointment_pk");

            entity.ToTable("appointment");

            entity.Property(e => e.IdAppointment)
                .ValueGeneratedNever()
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

        modelBuilder.Entity<Appointmentstatus>(entity =>
        {
            entity.HasKey(e => e.IdAppointmentStatus).HasName("appointmentstatus_pk");

            entity.ToTable("appointmentstatus");

            entity.Property(e => e.IdAppointmentStatus)
                .ValueGeneratedNever()
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
                .ValueGeneratedNever()
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
                .ValueGeneratedNever()
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
                .ValueGeneratedNever()
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
                .ValueGeneratedNever()
                .HasColumnName("idcandidate");
            entity.Property(e => e.DateOfBirth).HasColumnName("dateofbirth");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(200)
                .HasColumnName("emailaddress");
            entity.Property(e => e.Firstname)
                .HasMaxLength(200)
                .HasColumnName("firstname");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.HomeAddress)
                .HasMaxLength(255)
                .HasColumnName("homeaddress");
            entity.Property(e => e.IdNationality).HasColumnName("idnationality");
            entity.Property(e => e.IdStatus).HasColumnName("idstatus");
            entity.Property(e => e.IdStudyProgramme).HasColumnName("idstudyprogramme");
            entity.Property(e => e.Lastname)
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

            entity.HasOne(d => d.NationalityNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.IdNationality)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("candidate_nationality");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.IdStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_status");

            entity.HasOne(d => d.StudyProgrammeNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.IdStudyProgramme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("candidate_studyprogrammer");
        });

        modelBuilder.Entity<StudyLevel>(entity =>
        {
            entity.HasKey(e => e.IdStudyLevel).HasName("studylevel_pk");

            entity.ToTable("studylevel");

            entity.Property(e => e.IdStudyLevel)
                .ValueGeneratedNever()
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
                .ValueGeneratedNever()
                .HasColumnName("idstudymode");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<StudyProgramme>(entity =>
        {
            entity.HasKey(e => e.IdStudyProgramme).HasName("studyprogrammer_pk");

            entity.ToTable("studyprogrammer");

            entity.Property(e => e.IdStudyProgramme)
                .ValueGeneratedNever()
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

            entity.HasMany(d => d.IdDocumentTypes).WithMany(p => p.StudyProgrammes)
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}