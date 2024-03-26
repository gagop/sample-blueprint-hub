using Gakko.API.DTOs;
using Gakko.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gakko.API.Controllers;

[ApiController]
[Route("api/recruitments")]
public class RecruitmentsController : ControllerBase
{
    private readonly IRecruitmentsService _recruitmentService;

    public RecruitmentsController(IRecruitmentsService recruitmentService)
    {
        _recruitmentService = recruitmentService;
    }

    /// <summary>
    ///     Endpoint used to start a new recruitment process for a candidate
    /// </summary>
    /// <param name="newCandidate">Candidate's personal details required to begin the recruitment process</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateRecruitment(CreateRecruitmentDto newCandidate)
    {
        var candidate = await _recruitmentService.CreateRecruitment(newCandidate);
        return StatusCode(StatusCodes.Status201Created, candidate);
    }

    /// <summary>
    ///     Endpoint used to check the current meeting time for a candidate
    /// </summary>
    /// <param name="idStudent">Candidate's id</param>
    /// <returns></returns>
    [HttpGet("recruitments/{idStudent:int}/currentAppointment")]
    public async Task<IActionResult> GetCurrentAppointmentInfo(int idStudent)
    {
        var result = await _recruitmentService.GetCurrentAppointment(idStudent);
        return Ok(result);
    }

    [HttpPost("recruitments/{idStudent:int}/currentAppointment")]
    public IActionResult CreateMeeting()
    {
        return Ok("3. Schedule a new meeting");
    }

    [HttpPut("meetings/{idMeeting:int}/status")]
    public IActionResult CancelMeeting(int idMeeting)
    {
        return Ok("4. Cancel a meeting");
    }

    [HttpPut("{idRecruitment}/documents/{idDocumentType:int}/status")]
    public IActionResult ConfirmDocuments()
    {
        return Ok("5. Confirm candidates documents");
    }

    [HttpPut("{idRecruitment:int}/status")]
    public IActionResult ConfirmAdmissionFeePayment()
    {
        return Ok("6. Confirm payment admission fee");
    }

    [HttpPut("/cancel-unfinished-recruitments")]
    public IActionResult CancelUnfinishedRegistrations()
    {
        return Ok("7. Cancel unfinished recruitments");
    }
}