using Gakko.API.Models;
using Gakko.API.Shared.Models;

namespace Gakko.API.Recruitment;

public interface IRecruitmentsService
{
    Task<Student> CreateRecruitmentAsync(CreateRecruitmentDto createRecruitmentDto);
    Task<Appointment> GetCurrentAppointmentAsync(int idStudent);
    Task<Appointment> CreateAppointmentAsync(int idStudent);
    Task CancelAppointmentAsync(int idStudent);
    Task ConfirmDocumentAsync(int idStudent, int idDocument);
    Task ConfirmAdmissionFeePaymentAsync(int idStudent);
    Task CancelOngoingRecruitmentsAsync();
}