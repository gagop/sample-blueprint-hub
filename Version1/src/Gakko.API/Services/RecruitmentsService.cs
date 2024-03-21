using Gakko.API.Context;
using Gakko.API.Models;

namespace Gakko.API.Services;

public class RecruitmentsService : IRecruitmentsService
{
    private readonly GakkoContext _dbContext;

    public RecruitmentsService(GakkoContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CreateRecruitment(Student newCandidate)
    {
        await Task.Delay(1000);

        return true;
    }
}