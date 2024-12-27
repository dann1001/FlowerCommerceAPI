using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FlowerCommerceAPI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Text;
using Microsoft.Extensions.Options;
using FlowerCommerceAPI.Data;
using FlowerCommerceAPI.Services;
using FlowerCommerceAPI.Models; // Replace with the correct namespace if different

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add Localization services
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Add supported cultures
var supportedCultures = new[] { "en-US", "fa-IR" }; // Example: English and Persian
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US");
    options.SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
    options.SupportedUICultures = options.SupportedCultures;
});

// Add Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Flower Commerce API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by your token in the text box below.\nExample: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    });

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

// Configure the DbContext with SQL Server.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add JWT Authentication services to the container.
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"] ?? throw new ArgumentNullException("JwtSettings:SecretKey"))),
    };
});

// Register services for JWT and password handling
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<PasswordService>();

// Register the ProductService for IProductService
builder.Services.AddScoped<IProductService, ProductService>();

// Register the WishlistService for IWishlistService
builder.Services.AddScoped<IWishlistService, WishlistService>();

// Configure authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
});

var app = builder.Build();

// Use Localization Middleware
var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable Authentication and Authorization middleware.
app.UseAuthentication();
app.UseAuthorization();

// Define a simple route for the root path
app.MapGet("/", () => Results.Content("Welcome to the Flower Commerce API!"));

// Map controllers for API endpoints
app.MapControllers();

// Ensure that the database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    // Make sure to apply migrations first
    context.Database.Migrate(); // Applies any pending migrations

    // Call the local function to seed data
    SeedData(context).Wait(); // Seed the data
}

app.Run();

// Local function to seed data
async Task SeedData(AppDbContext context)
{
    // Check if data is already seeded to prevent duplicates
    if (!context.Products.Any())
    {
        var product = new Product
        {
            CategoryId = 1,
            Translations = new List<ProductTranslation>
            {
                new ProductTranslation
                {
                    Language = "en-US",
                    Name = "Sample Product",
                    Description = "This is a sample product"
                },
                new ProductTranslation
                {
                    Language = "fa-IR",
                    Name = "محصول نمونه",
                    Description = "این یک محصول نمونه است"
                }
            }
        };

        context.Products.Add(product);
        await context.SaveChangesAsync();
    }
}
