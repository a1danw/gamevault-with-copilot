﻿using GameVault.Models;

namespace GameVault.Services;

/// <summary>
/// Service interface for managing games
/// </summary>
public interface IGameService
{
    /// <summary>
    /// Gets all games
    /// </summary>
    Task<List<GameDto>> GetAllAsync();
    
    /// <summary>
    /// Gets a game by ID
    /// </summary>
    Task<GameDto?> GetByIdAsync(int id);
    
    /// <summary>
    /// Creates a new game
    /// </summary>
    Task<GameDto> CreateAsync(CreateGameDto createGameDto);
    
    /// <summary>
    /// Updates a game
    /// </summary>
    Task<GameDto?> UpdateAsync(int id, UpdateGameDto updateGameDto);
    
    /// <summary>
    /// Deletes a game
    /// </summary>
    Task<bool> DeleteAsync(int id);
}

