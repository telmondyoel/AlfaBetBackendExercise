using AlfaBetBackendExercise.Database.Context;
using AlfaBetBackendExercise.Logic;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<EventsHandler>();
builder.Services.AddDbContext<AlfaBetDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AlfaBetDb")));

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();