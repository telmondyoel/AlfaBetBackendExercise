using AlfaBetBackendExercise.Authentication;
using AlfaBetBackendExercise.Database.Context;
using AlfaBetBackendExercise.Logic.Events;
using AlfaBetBackendExercise.Logic.Events.Reminders;
using AlfaBetBackendExercise.Logic.Events.Reminders.Sinks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddScoped<EventsHandler>();

builder.Services.AddDbContext<AlfaBetDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AlfaBetDb")));

builder.Services.AddHostedService<EventsRemindersBackgroundService>();
builder.Services.AddSingleton<IEventReminderSink, ConsoleEventReminderSink>();

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

builder.Services.AddIdentityCore<AlfaBetUser>()
    .AddEntityFrameworkStores<AlfaBetDbContext>()
    .AddApiEndpoints();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.MapIdentityApi<AlfaBetUser>().WithTags("Authentication");

app.Run();