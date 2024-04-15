namespace Gakko.Application.Interfaces;

public interface IAppointmentManagerService
{
    Task<DateOnly> ScheduleAppointmentForCandidate(int idCandidate);

    Task CancelAppointmentsForCandidate(int idCandidate);
}