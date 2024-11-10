using Microsoft.EntityFrameworkCore;
using FlowerCommerceAPI.Data; // Make sure to include this

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the DbContext with SQL Server and connection string from appsettings.json
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add controllers (required for API controllers)
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Remove or comment out the HTTPS redirection
// app.UseHttpsRedirection();

// Define a simple route for the root path
app.MapGet("/", () => Results.Content("Welcome to the Flower Commerce API!"));

// Map controllers for API endpoints
app.MapControllers();

app.Run();
