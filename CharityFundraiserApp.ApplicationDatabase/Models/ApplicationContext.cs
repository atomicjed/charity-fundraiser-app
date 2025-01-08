using Microsoft.EntityFrameworkCore;

namespace CharityFundraiserApp.ApplicationDatabase.Models;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }
    
    public DbSet<Player> Players { get; set; }
}

public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
}

public enum Role
{
    Traitor = 0,
    Faithful = 1,
    ClaudiaWinkleman = 2
}