using Microsoft.AspNetCore.Identity;
using FlowerCommerceAPI.Models; // Adjust the namespace according to your project structure

namespace FlowerCommerceAPI.Services
{
    public class PasswordService
    {
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        // Hash the password
        public string HashPassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        // Verify the password
        public bool VerifyPassword(User user, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
