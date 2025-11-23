using GameVault.Models;
using GameVault.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameVault.Controllers;

/// <summary>
/// Controller for managing games in the vault
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    // COMMENTED OUT - Now using Entity Framework Core instead of static list
    // Static in-memory list for testing purposes
    // NOTE: This will reset when the application restarts
    // In production, this would be replaced with a database/repository pattern
    // private static readonly List<Game> _games = new()
    // {
    //     new Game { Id = 1, Title = "The Legend of Zelda", Platform = "Switch" },
    //     new Game { Id = 2, Title = "God of War", Platform = "PlayStation" },
    //     new Game { Id = 3, Title = "Halo Infinite", Platform = "Xbox" }
    // };
    // 
    // private static int _nextId = 4; // Counter for auto-incrementing IDs

    private readonly IGameService _gameService;

    /// <summary>
    /// Initializes a new instance of the GamesController
    /// </summary>
    /// <param name="gameService">The game service for database operations</param>
    public GamesController(IGameService gameService)
    {
        _gameService = gameService;
    }
    
    /// <summary>
    /// Gets all games in the vault
    /// </summary>
    /// <returns>A list of all games</returns>
    /// <response code="200">Returns the list of games</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GameDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetGames()
    {
        // OLD CODE: return Ok(_games);
        var games = await _gameService.GetAllAsync();
        return Ok(games);
    }
    
    /// <summary>
    /// Gets a specific game by ID
    /// </summary>
    /// <param name="id">The game ID</param>
    /// <returns>The game details</returns>
    /// <response code="200">Returns the game</response>
    /// <response code="404">If the game is not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GameDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetGame(int id)
    {
        // OLD CODE: var game = _games.FirstOrDefault(g => g.Id == id);
        var game = await _gameService.GetByIdAsync(id);
        
        if (game == null)
        {
            return NotFound(new { message = $"Game with ID {id} not found" });
        }
        
        return Ok(game);
    }
    
    /// <summary>
    /// Adds a new game to the vault
    /// </summary>
    /// <param name="createGameDto">The game data to add</param>
    /// <returns>The created game</returns>
    /// <response code="201">Returns the newly created game</response>
    /// <response code="400">If the game data is invalid</response>
    [HttpPost]
    [ProducesResponseType(typeof(GameDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameDto createGameDto)
    {
        // OLD CODE - Manual validation (now handled by [Required] data annotations in DTOs):
        // if (string.IsNullOrWhiteSpace(game.Title))
        // {
        //     return BadRequest(new { message = "Title is required" });
        // }
        // 
        // if (string.IsNullOrWhiteSpace(game.Platform))
        // {
        //     return BadRequest(new { message = "Platform is required" });
        // }
        // 
        // var newGame = new Game
        // {
        //     Id = _nextId++,
        //     Title = game.Title,
        //     Platform = game.Platform
        // };
        // 
        // _games.Add(newGame);
        // 
        // return CreatedAtAction(nameof(GetGame), new { id = newGame.Id }, newGame);

        var created = await _gameService.CreateAsync(createGameDto);
        return CreatedAtAction(nameof(GetGame), new { id = created.Id }, created);
    }
    
    /// <summary>
    /// Updates an existing game in the vault
    /// </summary>
    /// <param name="id">The game ID to update</param>
    /// <param name="updateGameDto">The updated game data</param>
    /// <returns>The updated game</returns>
    /// <response code="200">Returns the updated game</response>
    /// <response code="404">If the game is not found</response>
    /// <response code="400">If the game data is invalid</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(GameDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateGame(int id, [FromBody] UpdateGameDto updateGameDto)
    {
        // OLD CODE - Manual validation and in-memory update:
        // if (string.IsNullOrWhiteSpace(game.Title))
        // {
        //     return BadRequest(new { message = "Title is required" });
        // }
        // 
        // if (string.IsNullOrWhiteSpace(game.Platform))
        // {
        //     return BadRequest(new { message = "Platform is required" });
        // }
        // 
        // var existingGame = _games.FirstOrDefault(g => g.Id == id);
        // 
        // if (existingGame == null)
        // {
        //     return NotFound(new { message = $"Game with ID {id} not found" });
        // }
        // 
        // existingGame.Title = game.Title;
        // existingGame.Platform = game.Platform;
        // 
        // return Ok(existingGame);

        var updated = await _gameService.UpdateAsync(id, updateGameDto);
        
        if (updated == null)
        {
            return NotFound(new { message = $"Game with ID {id} not found" });
        }
        
        return Ok(updated);
    }
    
    /// <summary>
    /// Deletes a game from the vault
    /// </summary>
    /// <param name="id">The game ID to delete</param>
    /// <returns>No content on success</returns>
    /// <response code="204">If the game was successfully deleted</response>
    /// <response code="404">If the game is not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGame(int id)
    {
        // OLD CODE - In-memory list deletion:
        // var game = _games.FirstOrDefault(g => g.Id == id);
        // 
        // if (game == null)
        // {
        //     return NotFound(new { message = $"Game with ID {id} not found" });
        // }
        // 
        // _games.Remove(game);
        // 
        // return NoContent();

        var result = await _gameService.DeleteAsync(id);
        
        if (!result)
        {
            return NotFound(new { message = $"Game with ID {id} not found" });
        }
        
        return NoContent();
    }
}

