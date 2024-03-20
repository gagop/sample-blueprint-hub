using Gakko.API.Models;

namespace Gakko.API.Services;

public interface IRecruitmentsService
{
    Task<bool> CreateRecruitment(Student newCandidate);
}