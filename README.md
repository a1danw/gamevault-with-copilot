# GameVault

A Blazor Server application for managing a game collection vault with REST API support.

## Technologies Used

- **.NET 10.0** - Blazor Server
- **Entity Framework Core 9.0** - ORM with SQL Server
- **SQL Server Express** - Database (localhost\SQLEXPRESS)
- **Scalar** - Modern API documentation UI
- **OpenAPI** - API specification

## Getting Started

### Prerequisites

- .NET 10.0 SDK
- SQL Server Express installed on `localhost\SQLEXPRESS`

### Database Setup

The application uses Entity Framework Core with SQL Server Express. Follow these steps to set up the database:

#### Initial Migration (First Time Setup)

```powershell
# Navigate to the project folder
cd GameVault

# Create the initial migration
dotnet ef migrations add InitialCreate

# Apply the migration to create the database
dotnet ef database update
```

This will create the `GameVaultDb` database on your SQL Server Express instance with the `Games` table.

---

## Entity Framework Migration Commands

### Common Migration Commands

```powershell
# Add a new migration (after making model changes)
dotnet ef migrations add <MigrationName>

# Apply all pending migrations to the database
dotnet ef database update

# Revert to a specific migration
dotnet ef database update <MigrationName>

# Remove the last migration (only if not applied to database yet)
dotnet ef migrations remove

# List all migrations
dotnet ef migrations list

# Generate SQL script for migrations (without executing)
dotnet ef migrations script

# Drop the database (WARNING: deletes all data)
dotnet ef database drop

# View migration information
dotnet ef migrations has-pending-model-changes
```

### Example Workflow: Adding a New Property

1. **Modify your model** (e.g., add a new property to `Game.cs`)
   ```csharp
   public decimal Price { get; set; }
   ```

2. **Create a migration**
   ```powershell
   dotnet ef migrations add AddPriceToGame
   ```

3. **Apply the migration**
   ```powershell
   dotnet ef database update
   ```

---

## Running the Application

```powershell
# Run the application
dotnet run --project GameVault

# Or with watch mode (auto-restart on file changes)
dotnet watch run --project GameVault
```

The application will start on:
- **HTTPS**: `https://localhost:7xxx`
- **HTTP**: `http://localhost:5xxx`

---

## API Documentation

Once the application is running, you can access the API documentation:

- **Scalar UI** (Modern Interactive Docs): `https://localhost:7xxx/scalar/v1`
- **OpenAPI JSON Specification**: `https://localhost:7xxx/openapi/v1.json`

### Available API Endpoints

- `GET /api/games` - Get all games
- `GET /api/games/{id}` - Get a specific game by ID
- `POST /api/games` - Create a new game
- `PUT /api/games/{id}` - Update an existing game
- `DELETE /api/games/{id}` - Delete a game

---

## Project Structure

```
GameVault/
├── Components/          # Blazor components and pages
│   ├── Pages/          # Routable Blazor pages
│   └── Layout/         # Layout components
├── Controllers/         # API controllers
│   └── GamesController.cs
├── Data/               # Database context
│   └── AppDbContext.cs
├── Models/             # Data models
│   └── Game.cs
├── Services/           # Business logic services
│   ├── IGameService.cs
│   └── GameService.cs
├── Migrations/         # EF Core migration files (auto-generated)
├── wwwroot/            # Static files
├── Program.cs          # Application entry point
└── appsettings.json    # Configuration
```

---

## Connection String

The database connection string is configured in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=GameVaultDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

If your SQL Server instance has a different name, update the `Server` value accordingly.

---

## Troubleshooting

### Migration Issues

**Problem**: "Build failed" when running migrations
```powershell
# Solution: Build the project first
dotnet build
dotnet ef migrations add YourMigration
```

**Problem**: "No DbContext was found"
```powershell
# Solution: Make sure you're in the GameVault project folder
cd GameVault
dotnet ef migrations add YourMigration
```

**Problem**: "Cannot connect to database"
- Verify SQL Server Express is running
- Check the connection string in `appsettings.json`
- Ensure you have permissions to create databases

### Application is Running (Build Conflicts)

**Problem**: "The process cannot access the file because it is being used by another process"
```powershell
# Solution: Stop the running application first
# Press Ctrl+C in the terminal where the app is running
# Or close the browser and wait a few seconds
```

---

## Development Notes

- The project uses **Scoped** services for database operations (one instance per HTTP request)
- All service methods are **async** for better performance
- Error handling includes **try-catch blocks** with logging for debugging
- The old in-memory static list is **commented out** in `GamesController.cs` for reference

---

## License

This project is for educational purposes.

