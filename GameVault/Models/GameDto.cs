namespace GameVault.Models;

/// <summary>
/// Data transfer object for game responses
/// </summary>
public class GameDto
{
    /// <summary>
    /// Gets or sets the unique identifier
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Gets or sets the title
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the platform
    /// </summary>
    public string Platform { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the genre
    /// </summary>
    public string? Genre { get; set; }
    
    /// <summary>
    /// Gets or sets the release year
    /// </summary>
    public int? ReleaseYear { get; set; }
    
    /// <summary>
    /// Gets or sets the description
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Gets or sets whether the game has been completed
    /// </summary>
    public bool IsCompleted { get; set; }
    
    /// <summary>
    /// Gets or sets the personal rating (1-5 stars)
    /// </summary>
    public int? Rating { get; set; }
}

