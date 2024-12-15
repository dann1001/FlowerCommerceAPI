using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FlowerCommerceAPI.Data;
using FlowerCommerceAPI.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Flower Commerce API", Version = "v1" });

    // <summary>
    // Add JWT Bearer Authentication configuration to Swagger UI. This enables Swagger to show the correct
    // authorization header and prompts users to input their JWT token for authorized routes.
    // </summary>
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by your token in the text box below.\nExample: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    });

    // <summary>
    // Add security requirement to Swagger UI for authenticated routes. This ensures that Swagger enforces the use of the Bearer token for authorized endpoints.
    // </summary>
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// <summary>
// Configure the DbContext with SQL Server. This sets up the application's connection to the database using the connection string from appsettings.json.
// </summary>
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// <summary>
// Add JWT Authentication services to the container. This sets up the default JWT authentication scheme and configures the necessary parameters for validating JWT tokens.
// </summary>
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"] ?? throw new ArgumentNullException("JwtSettings:SecretKey")))
    };
});

// <summary>
// Register the JwtService for handling JWT token generation. The service is responsible for creating JWT tokens that clients can use to authenticate themselves.
// </summary>
builder.Services.AddScoped<JwtService>();

// <summary>
// Register the PasswordService for handling password hashing and verification. This service will help to securely store and check user passwords.
// </summary>
builder.Services.AddScoped<PasswordService>();

// <summary>
// Configure authorization policies. These policies are used to enforce access control based on user roles. In this case, we define two roles: Admin and User.
// </summary>
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
});

var app = builder.Build();

// <summary>
// Configure the HTTP request pipeline. This block sets up Swagger UI for API documentation in the development environment and enables the middleware for authentication and authorization.
// </summary>
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// <summary>
// Enable HTTPS redirection in production environments to ensure secure communication over HTTPS.
// </summary>
app.UseHttpsRedirection();

// <summary>
// Enable Authentication and Authorization middleware. This ensures that requests are authenticated and authorized before accessing protected resources.
// </summary>
app.UseAuthentication();
app.UseAuthorization();

// <summary>
// Define a simple route for the root path to display a welcome message.
// </summary>
app.MapGet("/", () => Results.Content("Welcome to the Flower Commerce API!"));

// <summary>
// Map controllers for API endpoints. This sets up the routes that handle HTTP requests and are mapped to controller actions.
// </summary>
app.MapControllers();

// <summary>
// Run the application. This starts the web server and begins listening for incoming requests.
// </summary>
app.Run();
