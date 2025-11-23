using GameVault.Components;
using GameVault.Data;
using GameVault.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add controllers for API endpoints
builder.Services.AddControllers();

// Add OpenAPI services - generates the API specification (blueprint) for all controller endpoints
// OpenAPI is a standard format for describing REST APIs
builder.Services.AddOpenApi();

// Register Entity Framework Core with SQL Server Express
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register game service
builder.Services.AddScoped<IGameService, GameService>();

// NOTE: If you wanted to use Swagger UI instead of Scalar, you would:
// 1. Install NuGet package: Swashbuckle.AspNetCore
// 2. Replace AddOpenApi() with: builder.Services.AddSwaggerGen();
// 3. Replace MapOpenApi() and MapScalarApiReference() with:
//    app.UseSwagger();
//    app.UseSwaggerUI();
// Then access Swagger UI at: /swagger

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Map controller endpoints
app.MapControllers();

// Map OpenAPI endpoint - exposes the API specification as JSON
// Access at: /openapi/v1.json
app.MapOpenApi();

// Add Scalar UI for API documentation - modern alternative to Swagger UI
// Scalar provides a beautiful, interactive web interface to visualize and test your API endpoints
// Access at: /scalar/v1
app.MapScalarApiReference();

app.Run();