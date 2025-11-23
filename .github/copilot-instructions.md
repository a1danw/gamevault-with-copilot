# GitHub Copilot Instructions for GameVault

## Project Overview
GameVault is a Blazor Server application for managing a game collection vault. It provides both a web UI and a REST API for managing games.

## NuGet Packages in Use
- **Microsoft.AspNetCore.OpenApi** (v9.0.0) - Generates OpenAPI specifications
- **Scalar.AspNetCore** (v1.2.42) - Modern API documentation UI (alternative to Swagger)

## API Documentation URLs
- **Scalar UI**: `/scalar/v1` - Interactive API documentation
- **OpenAPI JSON**: `/openapi/v1.json` - Raw API specification

## General Guidance

### Best Practices
- **One class per file** - Always create separate files for each class, model, component, service, interface
- **Use XML summary comments** - Document all public classes, methods, and properties
- **Use dependency injection** - Create interface + implementation, register in `Program.cs`
- **Follow REST conventions** - Use proper HTTP verbs and status codes (200, 201, 204, 404, 400)
- **Add ProducesResponseType** - Document all possible API response codes

### File Organization
- `/Models` - Data models and DTOs with namespace `GameVault.Models`
- `/Controllers` - API controllers with namespace `GameVault.Controllers`
- `/Components/Pages` - Blazor pages
- `/Components/Shared` - Shared Blazor components
- `/Services` - Business logic services with namespace `GameVault.Services`

### Dependency Injection
- **Always use interfaces** - Create an interface (`IYourService`) and implementation (`YourService`)
- **Register in Program.cs** - Always register new services: `builder.Services.AddScoped<IYourService, YourService>()`
- **Inject via constructor** - Use constructor injection in controllers and services
- **Service lifetimes**:
  - `AddScoped` - Created once per request (recommended for web apps)
  - `AddTransient` - Created every time it's requested
  - `AddSingleton` - Created once for the application lifetime

```csharp
// In Program.cs - register services
builder.Services.AddScoped<IGameService, GameService>();

// In controllers - inject via constructor
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;
    
    public GamesController(IGameService gameService)
    {
        _gameService = gameService;
    }
}
```

## Development Workflow

### IMPORTANT: Always Plan Before Implementing
1. **Present a plan first** - Before writing any code, explain what you will do
2. **Wait for confirmation** - Only start implementing after user says "yes", "proceed", "go ahead", or confirms
3. **Explain changes** - When making changes, explain what's being modified and why
4. **Break down complex tasks** - For multi-step work, show all steps in the plan

### Example Plan Format:
```
## Plan for [Feature Name]

I will:
1. Create interface `IGameService.cs` in `/Services` folder
2. Create implementation `GameService.cs` with CRUD methods
3. Register service in `Program.cs` using AddScoped
4. Update `GamesController` to inject and use the service

Should I proceed?
```

**Never start coding without user confirmation!**

## Adding a New API Controller

**File**: `/Controllers/YourController.cs`

**NOTE**: Controllers should use services via dependency injection, not access data directly.

```csharp
using GameVault.Models;
using GameVault.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameVault.Controllers;

/// <summary>
/// Controller for managing [resource name]
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class YourController : ControllerBase
{
    private readonly IYourService _yourService;
    
    /// <summary>
    /// Initializes a new instance of the YourController
    /// </summary>
    /// <param name="yourService">The service for managing [resources]</param>
    public YourController(IYourService yourService)
    {
        _yourService = yourService;
    }
    
    /// <summary>
    /// Gets all [resources]
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<YourModel>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var items = _yourService.GetAll();
        return Ok(items);
    }
    
    /// <summary>
    /// Gets a [resource] by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(YourModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(int id)
    {
        var item = _yourService.GetById(id);
        
        if (item == null)
        {
            return NotFound(new { message = $"[Resource] with ID {id} not found" });
        }
        
        return Ok(item);
    }
    
    /// <summary>
    /// Creates a new [resource]
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(YourModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] YourModel model)
    {
        var created = _yourService.Create(model);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    
    /// <summary>
    /// Updates a [resource]
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(YourModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(int id, [FromBody] YourModel model)
    {
        var updated = _yourService.Update(id, model);
        
        if (updated == null)
        {
            return NotFound(new { message = $"[Resource] with ID {id} not found" });
        }
        
        return Ok(updated);
    }
    
    /// <summary>
    /// Deletes a [resource]
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        var result = _yourService.Delete(id);
        
        if (!result)
        {
            return NotFound(new { message = $"[Resource] with ID {id} not found" });
        }
        
        return NoContent();
    }
}
```

## Adding a New Model

**File**: `/Models/YourModel.cs`

```csharp
using System.ComponentModel.DataAnnotations;

namespace GameVault.Models;

/// <summary>
/// Represents a [description]
/// </summary>
public class YourModel
{
    /// <summary>
    /// Gets or sets the unique identifier
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Gets or sets the name
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the description
    /// </summary>
    public string? Description { get; set; }
}
```

## Adding a New Service (with Dependency Injection)

**ALWAYS create services with interface + implementation pattern**

### Step 1: Create Interface
**File**: `/Services/IYourService.cs`

```csharp
using GameVault.Models;

namespace GameVault.Services;

/// <summary>
/// Service interface for managing [resource]
/// </summary>
public interface IYourService
{
    /// <summary>
    /// Gets all [resources]
    /// </summary>
    List<YourModel> GetAll();
    
    /// <summary>
    /// Gets a [resource] by ID
    /// </summary>
    YourModel? GetById(int id);
    
    /// <summary>
    /// Creates a new [resource]
    /// </summary>
    YourModel Create(YourModel model);
    
    /// <summary>
    /// Updates a [resource]
    /// </summary>
    YourModel? Update(int id, YourModel model);
    
    /// <summary>
    /// Deletes a [resource]
    /// </summary>
    bool Delete(int id);
}
```

### Step 2: Create Implementation
**File**: `/Services/YourService.cs`

```csharp
using GameVault.Models;

namespace GameVault.Services;

/// <summary>
/// Service implementation for managing [resource]
/// </summary>
public class YourService : IYourService
{
    private readonly ILogger<YourService> _logger;
    
    /// <summary>
    /// Initializes a new instance of the YourService
    /// </summary>
    public YourService(ILogger<YourService> logger)
    {
        _logger = logger;
    }
    
    /// <inheritdoc />
    public List<YourModel> GetAll()
    {
        _logger.LogInformation("Getting all [resources]");
        // Implementation
        return new List<YourModel>();
    }
    
    /// <inheritdoc />
    public YourModel? GetById(int id)
    {
        _logger.LogInformation("Getting [resource] with ID {Id}", id);
        // Implementation
        return null;
    }
    
    /// <inheritdoc />
    public YourModel Create(YourModel model)
    {
        _logger.LogInformation("Creating new [resource]");
        // Implementation
        return model;
    }
    
    /// <inheritdoc />
    public YourModel? Update(int id, YourModel model)
    {
        _logger.LogInformation("Updating [resource] with ID {Id}", id);
        // Implementation
        return null;
    }
    
    /// <inheritdoc />
    public bool Delete(int id)
    {
        _logger.LogInformation("Deleting [resource] with ID {Id}", id);
        // Implementation
        return false;
    }
}
```

### Step 3: Register in Program.cs
**ALWAYS register your new service!**

```csharp
// Add this line in Program.cs before var app = builder.Build();
builder.Services.AddScoped<IYourService, YourService>();
```

### Step 4: Inject into Controller or Component
```csharp
// In controller
public class YourController : ControllerBase
{
    private readonly IYourService _yourService;
    
    public YourController(IYourService yourService)
    {
        _yourService = yourService;
    }
}

// In Blazor component
@inject IYourService YourService
```

## Adding a New Blazor Component

**File**: `/Components/Pages/YourPage.razor` (for pages)
**File**: `/Components/Shared/YourComponent.razor` (for shared components)

```razor
@page "/your-route"
@using GameVault.Models

@* 
    Component: YourPage
    Description: [What this does]
*@

<PageTitle>Your Title</PageTitle>

<h3>Your Heading</h3>

@if (isLoading)
{
    <p><em>Loading...</em></p>
}
else
{
    <div>
        @* Your content here *@
    </div>
}

@code {
    private bool isLoading = true;
    
    /// <summary>
    /// Initializes the component
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        isLoading = true;
        // Load your data
        isLoading = false;
    }
}
```


