using GameVault.Models;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Data;

/// <summary>
/// Application database context for GameVault
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the AppDbContext
    /// </summary>
    /// <param name="options">The DbContext options</param>
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the Games collection
    /// </summary>
    public DbSet<Game> Games { get; set; } = null!;
}

