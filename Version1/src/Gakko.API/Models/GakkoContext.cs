using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Gakko.API.Models;

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

    public virtual DbSet<Documenttype> Documenttypes { get; set; }

    public virtual DbSet<Nationality> Nationalities { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Studylevel> Studylevels { get; set; }

    public virtual DbSet<Studymode> Studymodes { get; set; }

    public virtual DbSet<Studyprogrammer> Studyprogrammers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=gakko;User Id=postgres;Password=x@4K|9Ro;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Idappointment).HasName("appointment_pk");

            entity.ToTable("appointment");

            entity.Property(e => e.Idappointment)
                .ValueGeneratedNever()
                .HasColumnName("idappointment");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Idappointmentstatus).HasColumnName("idappointmentstatus");
            entity.Property(e => e.Idcandidate).HasColumnName("idcandidate");

            entity.HasOne(d => d.IdappointmentstatusNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.Idappointmentstatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("appointment_appointmentstatus");

            entity.HasOne(d => d.IdcandidateNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.Idcandidate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("appointment_candidate");
        });

        modelBuilder.Entity<Appointmentstatus>(entity =>
        {
            entity.HasKey(e => e.Idappointmentstatus).HasName("appointmentstatus_pk");

            entity.ToTable("appointmentstatus");

            entity.Property(e => e.Idappointmentstatus)
                .ValueGeneratedNever()
                .HasColumnName("idappointmentstatus");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Documenttype>(entity =>
        {
            entity.HasKey(e => e.Iddocumenttype).HasName("documenttype_pk");

            entity.ToTable("documenttype");

            entity.Property(e => e.Iddocumenttype)
                .ValueGeneratedNever()
                .HasColumnName("iddocumenttype");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Nationality>(entity =>
        {
            entity.HasKey(e => e.Idnationality).HasName("nationality_pk");

            entity.ToTable("nationality");

            entity.Property(e => e.Idnationality)
                .ValueGeneratedNever()
                .HasColumnName("idnationality");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Idstatus).HasName("status_pk");

            entity.ToTable("status");

            entity.Property(e => e.Idstatus)
                .ValueGeneratedNever()
                .HasColumnName("idstatus");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Idcandidate).HasName("student_pk");

            entity.ToTable("student");

            entity.Property(e => e.Idcandidate)
                .ValueGeneratedNever()
                .HasColumnName("idcandidate");
            entity.Property(e => e.Dateofbirth).HasColumnName("dateofbirth");
            entity.Property(e => e.Emailaddress)
                .HasMaxLength(200)
                .HasColumnName("emailaddress");
            entity.Property(e => e.Firstname)
                .HasMaxLength(200)
                .HasColumnName("firstname");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Homeaddress)
                .HasMaxLength(255)
                .HasColumnName("homeaddress");
            entity.Property(e => e.Idnationality).HasColumnName("idnationality");
            entity.Property(e => e.Idstatus).HasColumnName("idstatus");
            entity.Property(e => e.Idstudyprogramme).HasColumnName("idstudyprogramme");
            entity.Property(e => e.Lastname)
                .HasMaxLength(200)
                .HasColumnName("lastname");
            entity.Property(e => e.Passportnumber)
                .HasMaxLength(9)
                .HasColumnName("passportnumber");
            entity.Property(e => e.Peselnumber)
                .HasMaxLength(11)
                .HasColumnName("peselnumber");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(200)
                .HasColumnName("phonenumber");

            entity.HasOne(d => d.IdnationalityNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.Idnationality)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("candidate_nationality");

            entity.HasOne(d => d.IdstatusNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.Idstatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_status");

            entity.HasOne(d => d.IdstudyprogrammeNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.Idstudyprogramme)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("candidate_studyprogrammer");
        });

        modelBuilder.Entity<Studylevel>(entity =>
        {
            entity.HasKey(e => e.Idstudylevel).HasName("studylevel_pk");

            entity.ToTable("studylevel");

            entity.Property(e => e.Idstudylevel)
                .ValueGeneratedNever()
                .HasColumnName("idstudylevel");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Studymode>(entity =>
        {
            entity.HasKey(e => e.Idstudymode).HasName("studymode_pk");

            entity.ToTable("studymode");

            entity.Property(e => e.Idstudymode)
                .ValueGeneratedNever()
                .HasColumnName("idstudymode");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Studyprogrammer>(entity =>
        {
            entity.HasKey(e => e.Idstudyprogramme).HasName("studyprogrammer_pk");

            entity.ToTable("studyprogrammer");

            entity.Property(e => e.Idstudyprogramme)
                .ValueGeneratedNever()
                .HasColumnName("idstudyprogramme");
            entity.Property(e => e.Idstudylevel).HasColumnName("idstudylevel");
            entity.Property(e => e.Idstudymode).HasColumnName("idstudymode");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Recruitmentend).HasColumnName("recruitmentend");
            entity.Property(e => e.Recruitmentstart).HasColumnName("recruitmentstart");

            entity.HasOne(d => d.IdstudylevelNavigation).WithMany(p => p.Studyprogrammers)
                .HasForeignKey(d => d.Idstudylevel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("studyprogrammer_studycycle");

            entity.HasOne(d => d.IdstudymodeNavigation).WithMany(p => p.Studyprogrammers)
                .HasForeignKey(d => d.Idstudymode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("studyprogrammer_studymode");

            entity.HasMany(d => d.Iddocumenttypes).WithMany(p => p.Idstudyprogrammes)
                .UsingEntity<Dictionary<string, object>>(
                    "Requiredenrollmentdocument",
                    r => r.HasOne<Documenttype>().WithMany()
                        .HasForeignKey("Iddocumenttype")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("requiredenrollmentdocument_documenttype"),
                    l => l.HasOne<Studyprogrammer>().WithMany()
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
