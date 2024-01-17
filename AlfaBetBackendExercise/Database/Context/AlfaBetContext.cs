using AlfaBetBackendExercise.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlfaBetBackendExercise.Database.Context;

public class AlfaBetContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Event> Events { get; set; }
}