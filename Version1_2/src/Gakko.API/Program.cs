using System.Reflection;
using Gakko.API.Recruitment;
using Gakko.API.Shared;
using Gakko.API.Shared.Middlewares;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(ctx =>
{
    ctx.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Gakko API - version 1",
        Version = "v1",
        Description = "API used as an example of various ways to implement business requirements."
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    ctx.IncludeXmlComments(xmlPath);
});

builder.Services.RegisterRecruitmentServices();
builder.Services.RegisterSharedServices(builder.Configuration);

var app = builder.Build();

app.UseExceptionMiddleware();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.RegisterRecruitmentEndpoints();

app.Run();