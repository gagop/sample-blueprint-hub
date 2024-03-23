using Gakko.API.DTOs;
using Gakko.API.Models;

namespace Gakko.API.Services;

public interface IRecruitmentsService
{
    Task<Student> CreateRecruitment(CreateRecruitmentDto createRecruitmentDto);
}