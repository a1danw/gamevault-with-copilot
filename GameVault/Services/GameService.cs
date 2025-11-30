using GameVault.Data;
using GameVault.Models;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Services;

/// <summary>
/// Service implementation for managing games using Entity Framework Core
/// </summary>
public class GameService : IGameService
{
    private readonly AppDbContext _context;
    private readonly ILogger<GameService> _logger;

    /// <summary>
    /// Initializes a new instance of the GameService
    /// </summary>
    /// <param name="context">The database context</param>
    /// <param name="logger">The logger</param>
    public GameService(AppDbContext context, ILogger<GameService> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Maps a Game entity to a GameDto
    /// </summary>
    private static GameDto MapToDto(Game game)
    {
        return new GameDto
        {
            Id = game.Id,
            Title = game.Title,
            Platform = game.Platform,
            Genre = game.Genre,
            ReleaseYear = game.ReleaseYear,
            Description = game.Description,
            IsCompleted = game.IsCompleted,
            Rating = game.Rating
        };
    }

    /// <summary>
    /// Maps a CreateGameDto to a Game entity
    /// </summary>
    private static Game MapToEntity(CreateGameDto dto)
    {
        return new Game
        {
            Title = dto.Title,
            Platform = dto.Platform,
            Genre = dto.Genre,
            ReleaseYear = dto.ReleaseYear,
            Description = dto.Description,
            IsCompleted = dto.IsCompleted,
            Rating = dto.Rating
        };
    }

    /// <summary>
    /// Updates a Game entity from an UpdateGameDto
    /// </summary>
    private static void UpdateEntityFromDto(Game game, UpdateGameDto dto)
    {
        game.Title = dto.Title;
        game.Platform = dto.Platform;
        game.Genre = dto.Genre;
        game.ReleaseYear = dto.ReleaseYear;
        game.Description = dto.Description;
        game.IsCompleted = dto.IsCompleted;
        game.Rating = dto.Rating;
    }

    /// <inheritdoc />
    public async Task<List<GameDto>> GetAllAsync()
    {
        try
        {
            _logger.LogInformation("Getting all games from database");
            var games = await _context.Games.AsNoTracking().ToListAsync();
            return games.Select(MapToDto).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve games from database");
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<GameDto?> GetByIdAsync(int id)
    {
        try
        {
            _logger.LogInformation("Getting game with ID {Id} from database", id);
            var game = await _context.Games.FindAsync(id);
            return game == null ? null : MapToDto(game);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting game with ID {Id} from database", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<GameDto> CreateAsync(CreateGameDto createGameDto)
    {
        try
        {
            _logger.LogInformation("Creating new game: {Title} ({Platform})", createGameDto.Title, createGameDto.Platform);
            
            var game = MapToEntity(createGameDto);
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Successfully created game with ID {Id}", game.Id);
            return MapToDto(game);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database constraint error while creating game: {Title}", createGameDto.Title);
            throw new InvalidOperationException($"Could not save game '{createGameDto.Title}' due to database constraint", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error creating game: {Title}", createGameDto.Title);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<GameDto?> UpdateAsync(int id, UpdateGameDto updateGameDto)
    {
        try
        {
            _logger.LogInformation("Updating game with ID {Id}", id);
            
            var existing = await _context.Games.FindAsync(id);
            if (existing == null)
            {
                _logger.LogWarning("Game with ID {Id} not found for update", id);
                return null;
            }

            UpdateEntityFromDto(existing, updateGameDto);

            await _context.SaveChangesAsync();
            _logger.LogInformation("Successfully updated game ID {Id}: {Title}", id, updateGameDto.Title);
            return MapToDto(existing);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogError(ex, "Concurrency conflict updating game ID {Id}", id);
            throw new InvalidOperationException($"Game with ID {id} was modified by another user. Please refresh and try again.", ex);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database error updating game ID {Id}", id);
            throw new InvalidOperationException($"Could not update game ID {id} due to database constraint", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error updating game ID {Id}", id);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            _logger.LogInformation("Attempting to delete game with ID {Id}", id);
            
            var existing = await _context.Games.FindAsync(id);
            if (existing == null)
            {
                _logger.LogWarning("Game with ID {Id} not found for deletion", id);
                return false;
            }

            _context.Games.Remove(existing);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Successfully deleted game ID {Id}: {Title}", id, existing.Title);
            return true;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database error deleting game ID {Id} - may be referenced by other records", id);
            throw new InvalidOperationException($"Cannot delete game ID {id} because it is referenced by other records", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error deleting game ID {Id}", id);
            throw;
        }
    }
}

