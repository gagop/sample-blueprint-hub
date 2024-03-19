using Microsoft.AspNetCore.Mvc;

namespace Gakko.API.Controllers;

[ApiController]
[Route("api/recruitments")]
public class RecruitmentsController : ControllerBase
{

    [HttpPost]
    public IActionResult CreateRecruitment()
    {
        return Ok("1. Register for studies ");
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