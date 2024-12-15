using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FlowerCommerceAPI.Services
{
    public class JwtService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private const int TokenLifetime = 30; // Default to 30 minutes for regular sessions
        private const int RememberMeLifetime = 1440; // 1 day for "Remember Me"

        public JwtService(IConfiguration configuration)
        {
            // Initialize configuration values with null checks
            _secretKey = configuration["JwtSettings:SecretKey"] ?? throw new ArgumentNullException(nameof(_secretKey), "Secret Key is missing in configuration.");
            _issuer = configuration["JwtSettings:Issuer"] ?? throw new ArgumentNullException(nameof(_issuer), "Issuer is missing in configuration.");
            _audience = configuration["JwtSettings:Audience"] ?? throw new ArgumentNullException(nameof(_audience), "Audience is missing in configuration.");
        }

        /// <summary>
        /// Generates a JWT token for the specified user.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="role">The role of the user.</param>
        /// <param name="rememberMe">Whether to extend the token's expiration time.</param>
        /// <returns>A JWT token as a string.</returns>
       public string GenerateToken(string username, string role, int userId)
{
    if (string.IsNullOrEmpty(username))
        throw new ArgumentException("Username cannot be null or empty.", nameof(username));

    if (string.IsNullOrEmpty(role))
        throw new ArgumentException("Role cannot be null or empty.", nameof(role));

    // Define claims for the token
    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, username), // Use 'username' directly
        new Claim("Id", userId.ToString()), // Use 'userId' passed as parameter
        new Claim(ClaimTypes.Name, username), 
        new Claim(ClaimTypes.Role, role),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Unique identifier for the token
    };

    // Determine the expiration time based on "rememberMe" flag
    // var expires = DateTime.UtcNow.AddMinutes(rememberMe ? RememberMeLifetime : TokenLifetime);
  

    // Generate a security key
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    // Create the token
    var token = new JwtSecurityToken(
        issuer: _issuer,
        audience: _audience,
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(1600),
        signingCredentials: creds
    );

    // Return the serialized token
    return new JwtSecurityTokenHandler().WriteToken(token);
}

    }
}
