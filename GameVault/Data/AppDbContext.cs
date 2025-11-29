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

    /// <summary>
    /// Configures the model and seeds initial data
    /// </summary>
    /// <param name="modelBuilder">The model builder</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed initial game data
        modelBuilder.Entity<Game>().HasData(
            new Game
            {
                Id = 100,
                Title = "The Legend of Zelda: Breath of the Wild",
                Platform = "Nintendo Switch",
                Genre = "Action-Adventure",
                ReleaseYear = 2017,
                Description = "An open-world action-adventure game set in the kingdom of Hyrule."
            },
            new Game
            {
                Id = 101,
                Title = "Red Dead Redemption 2",
                Platform = "PlayStation 5",
                Genre = "Action-Adventure",
                ReleaseYear = 2018,
                Description = "An epic tale of life in America's unforgiving heartland."
            },
            new Game
            {
                Id = 102,
                Title = "Hades",
                Platform = "PC",
                Genre = "Roguelike",
                ReleaseYear = 2020,
                Description = "A rogue-like dungeon crawler where you defy the god of the dead."
            },
            new Game
            {
                Id = 103,
                Title = "Elden Ring",
                Platform = "PC",
                Genre = "Action RPG",
                ReleaseYear = 2022,
                Description = "A fantasy action RPG developed by FromSoftware and George R.R. Martin."
            },
            new Game
            {
                Id = 104,
                Title = "Celeste",
                Platform = "Nintendo Switch",
                Genre = "Platformer",
                ReleaseYear = 2018,
                Description = "A challenging platformer about climbing a mountain and overcoming personal struggles."
            }
        );
    }
}

