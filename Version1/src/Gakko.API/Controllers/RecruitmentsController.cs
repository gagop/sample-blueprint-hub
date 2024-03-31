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
    ///     Endpoint used to start a new recruitment process for a candidate.
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
    ///     Endpoint used to check the current appointment time for a candidate.
    /// </summary>
    /// <param name="idStudent">Candidate's id</param>
    /// <returns></returns>
    [HttpGet("recruitments/{idStudent:int}/currentAppointment")]
    public async Task<IActionResult> GetCurrentAppointmentInfo(int idStudent)
    {
        var result = await _recruitmentService.GetCurrentAppointment(idStudent);
        return Ok(result);
    }


    /// <summary>
    ///     Create new appointment for a candidate and cancel any previous appointments.
    /// </summary>
    /// <returns></returns>
    [HttpPost("recruitments/{idStudent:int}/appointments")]
    public async Task<IActionResult> CreateAppointment(int idStudent)
    {
        var appointment = await _recruitmentService.CreateAppointment(idStudent);
        return StatusCode(StatusCodes.Status201Created, appointment);
    }

    /// <summary>
    ///     Cancel the currently scheduled appointment for a candidate.
    /// </summary>
    /// <param name="idStudent">Id of the candidate</param>
    /// <returns>204 - No content</returns>
    [HttpPut("recruitments/{idStudent:int}")]
    public async Task<IActionResult> CancelAppointment(int idStudent)
    {
        await _recruitmentService.CancelAppointment(idStudent);
        return NoContent();
    }

    /// <summary>
    ///     Confirm the candidate's documents for the recruitment process.
    ///     When all the documents required by specific study programme are submitted, the candidate is moved to the next stage
    ///     of the recruitment process.
    /// </summary>
    /// <param name="idStudent">Id of the candidate</param>
    /// <param name="idDocumentType">Id of the document type</param>
    /// <returns>204 - No content</returns>
    [HttpPut("recruitments/{idStudent:int}/documents/{idDocumentType:int")]
    public async Task<IActionResult> ConfirmDocuments(int idStudent, int idDocumentType)
    {
        await _recruitmentService.ConfirmDocument(idStudent, idDocumentType);
        return NoContent();
    }

    /// <summary>
    ///     Confirm that the admission fee has been paid by the candidate.
    ///     Then student is moved to the student status and we assign index number to him.
    /// </summary>
    /// <param name="idStudent">Id of the candidate</param>
    /// <returns>204 - No content</returns>
    [HttpPut("recruitments/{idStudent:int}/admission-fee")]
    public async Task<IActionResult> ConfirmAdmissionFeePayment(int idStudent)
    {
        await _recruitmentService.ConfirmAdmissionFeePayment(idStudent);
        return NoContent();
    }

    /// <summary>
    ///     Cancels all the ongoing recruitment processes which are not finished.
    ///     All the candidates are moved to "Candidate - cancelled" status.
    ///     All the scheduled appointments are cancelled.
    /// </summary>
    /// <returns>204 - No content</returns>
    [HttpPut("recruitments/unfinished")]
    public async Task<IActionResult> CancelUnfinishedRegistrations()
    {
        await _recruitmentService.CancelOngoingRecruitments();
        return NoContent();
    }
}