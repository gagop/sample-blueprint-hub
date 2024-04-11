namespace Gakko.API.Recruitment;

public static class Configuration
{
    public static void RegisterRecruitmentEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/recruitments",
            async (IRecruitmentsService service, CreateRecruitmentDto CreateRecruitmentDto) =>
            {
                var candidate = await service.CreateRecruitmentAsync(CreateRecruitmentDto);
                return Results.Created($"api/recruitments/{candidate.IdCandidate:int}", candidate);
            }).WithName("CreateRecruitment").WithOpenApi();

        app.MapGet("api/recruitments/{idStudent:int}/currentAppointment",
            async (IRecruitmentsService service, int idStudent) =>
            {
                var result = await service.GetCurrentAppointmentAsync(idStudent);
                return Results.Ok(result);
            }).WithName("GetCurrentAppointmentInfo").WithOpenApi();

        app.MapPost("api/recruitments/{idStudent:int}/appointments",
            async (IRecruitmentsService service, int idStudent) =>
            {
                var appointment = await service.CreateAppointmentAsync(idStudent);
                return Results.Created("/api/recruitments/{idStudent:int}/currentAppointment", appointment);
            }).WithName("CreateAppointment").WithOpenApi();

        app.MapPut("api/recruitments/{idStudent:int}/currentAppointment",
            async (IRecruitmentsService service, int idStudent) =>
            {
                await service.CancelAppointmentAsync(idStudent);
                return Results.NoContent();
            }).WithName("CancelAppointment").WithOpenApi();

        app.MapPut("api/recruitments/{idStudent:int}/documents/{idDocumentType:int}",
            async (IRecruitmentsService service, int idStudent, int idDocumentType) =>
            {
                await service.ConfirmDocumentAsync(idStudent, idDocumentType);
                return Results.NoContent();
            }).WithName("ConfirmDocuments").WithOpenApi();

        app.MapPut("api/recruitments/{idStudent:int}/admission-fee",
            async (IRecruitmentsService service, int idStudent) =>
            {
                await service.ConfirmAdmissionFeePaymentAsync(idStudent);
                return Results.NoContent();
            }).WithName("ConfirmAdmissionFeePayment").WithOpenApi();

        app.MapPut("api/recruitments/unfinished",
            async (IRecruitmentsService service) =>
            {
                await service.CancelOngoingRecruitmentsAsync();
                return Results.NoContent();
            }).WithName("CancelUnfinishedRegistrations").WithOpenApi();
    }

    public static void RegisterRecruitmentServices(this IServiceCollection services)
    {
        services.AddScoped<IRecruitmentsService, RecruitmentsService>();
    }
}