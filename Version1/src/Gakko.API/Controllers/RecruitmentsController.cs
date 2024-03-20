using Gakko.API.Models;
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

    [HttpPost]
    public async Task<IActionResult> CreateRecruitment(Student newCandidate)
    {
        var result = await _recruitmentService.CreateRecruitment(newCandidate);

        if (!result) return BadRequest("Recruitment process cannot be started");

        return StatusCode(StatusCodes.Status201Created, "Recruitment process was started successfully");
    }

    [HttpGet("meetings/current")]
    public IActionResult GetMeetingInfo()
    {
        return Ok("2. Display current meeting time");
    }

    [HttpPost("meetings")]
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