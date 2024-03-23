using Gakko.API.Context;
using Gakko.API.Middlewares;
using Gakko.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<GakkoContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("GakkoDb")));
builder.Services.AddScoped<IRecruitmentsService, RecruitmentsService>();

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