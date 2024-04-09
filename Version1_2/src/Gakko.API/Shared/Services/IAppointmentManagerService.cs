namespace Gakko.API.Shared.Services;

public interface IAppointmentManagerService
{
    Task<DateOnly> ScheduleAppointmentForCandidate(int idCandidate);

    Task CancelAppointmentsForCandidate(int idCandidate);
}