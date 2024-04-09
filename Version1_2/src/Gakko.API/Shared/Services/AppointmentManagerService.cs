namespace Gakko.API.Shared.Services;

/// <summary>
///     The following class is simulating a call to the remote service responsible for
///     managing appointments and scheduling them for candidates.
/// </summary>
public class AppointmentManagerService : IAppointmentManagerService
{
    public async Task<DateOnly> ScheduleAppointmentForCandidate(int idCandidate)
    {
        // Simulating a call to the remote service
        var randomWaitTime = new Random().Next(200, 2000);
        await Task.Delay(randomWaitTime);
        return DateOnly.FromDateTime(DateTime.Now.AddDays(7));
    }

    public async Task CancelAppointmentsForCandidate(int idCandidate)
    {
        // Simulating a call to the remote service
        var randomWaitTime = new Random().Next(200, 2000);
        await Task.Delay(randomWaitTime);
    }
}