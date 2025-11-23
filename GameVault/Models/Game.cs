using System.ComponentModel.DataAnnotations;

namespace GameVault.Models;

/// <summary>
/// Represents a game in the vault
/// </summary>
public class Game
{
    /// <summary>
    /// Gets or sets the unique identifier for the game
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Gets or sets the title of the game
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the platform the game is available on
    /// </summary>
    [StringLength(100)]
    public string Platform { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the genre
    /// </summary>
    [StringLength(100)]
    public string? Genre { get; set; }
    
    /// <summary>
    /// Gets or sets the release year
    /// </summary>
    public int? ReleaseYear { get; set; }
    
    /// <summary>
    /// Gets or sets the description
    /// </summary>
    public string? Description { get; set; }
}

