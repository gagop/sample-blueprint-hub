using Gakko.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gakko.Application.Interfaces;

public interface IDbContext
{
    DbSet<Appointment> Appointments { get; set; }
    DbSet<AppointmentStatus> AppointmentStatuses { get; set; }
    DbSet<DocumentType> DocumentTypes { get; set; }
    DbSet<CandidatesDocument> CandidatesDocument { get; set; }
    DbSet<Nationality> Nationalities { get; set; }
    DbSet<Status> Statuses { get; set; }
    DbSet<Student> Students { get; set; }
    DbSet<StudyLevel> StudyLevels { get; set; }
    DbSet<StudyMode> StudyModes { get; set; } 
    DbSet<StudyProgramme> StudyProgrammes { get; set; }
    Task<int> SaveChangesAsync(CancellationToken token=default);
}