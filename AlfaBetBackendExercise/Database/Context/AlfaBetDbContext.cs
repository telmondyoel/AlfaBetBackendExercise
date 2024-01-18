using AlfaBetBackendExercise.Authentication;
using AlfaBetBackendExercise.Database.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlfaBetBackendExercise.Database.Context;

public class AlfaBetDbContext(DbContextOptions options) : IdentityDbContext<AlfaBetUser>(options)
{
    public DbSet<Event> Events { get; set; }
}