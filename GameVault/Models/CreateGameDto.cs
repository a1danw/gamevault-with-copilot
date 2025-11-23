using System.ComponentModel.DataAnnotations;

namespace GameVault.Models;

/// <summary>
/// Data transfer object for creating a new game
/// </summary>
public class CreateGameDto
{
    /// <summary>
    /// Gets or sets the title
    /// </summary>
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the platform
    /// </summary>
    [Required(ErrorMessage = "Platform is required")]
    [StringLength(100, ErrorMessage = "Platform cannot exceed 100 characters")]
    public string Platform { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the genre
    /// </summary>
    [StringLength(100, ErrorMessage = "Genre cannot exceed 100 characters")]
    public string? Genre { get; set; }
    
    /// <summary>
    /// Gets or sets the release year
    /// </summary>
    [Range(1970, 2100, ErrorMessage = "Release year must be between 1970 and 2100")]
    public int? ReleaseYear { get; set; }
    
    /// <summary>
    /// Gets or sets the description
    /// </summary>
    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string? Description { get; set; }
}

