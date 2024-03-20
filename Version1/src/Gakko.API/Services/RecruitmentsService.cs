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
        /*
         * he candidate must provide their first name, last name, phone number, email address, home address, the study program they selected from our offerings, gender,
         * PESEL number or passport number, nationality, and date of birth.
         */


        await Task.Delay(1000);

        return true;
    }
}