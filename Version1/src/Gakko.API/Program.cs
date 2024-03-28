using System.Reflection;
using Gakko.API.Context;
using Gakko.API.Middlewares;
using Gakko.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

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

builder.Services.AddControllers()
    .AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });

builder.Services.AddDbContext<GakkoContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("GakkoDb")));
builder.Services.AddScoped<IRecruitmentsService, RecruitmentsService>();
builder.Services.AddScoped<IAppointmentManagerService, AppointmentManagerService>();
builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

app.UseExceptionMiddleware();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();