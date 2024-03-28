namespace Gakko.API.Services;

public interface IAppointmentManagerService
{
    Task<DateOnly> ScheduleAppointmentForCandidate(int idCandidate);

    Task CancelAppointmentsForCandidate(int idCandidate);
}