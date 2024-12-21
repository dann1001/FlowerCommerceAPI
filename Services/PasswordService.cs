using Microsoft.AspNetCore.Identity;
using FlowerCommerceAPI.Models; // Adjust the namespace according to your project structure

namespace FlowerCommerceAPI.Services
{
    /// <summary>
    /// Service for handling password-related operations such as hashing and verification.
    /// </summary>
    public class PasswordService
    {
        private readonly PasswordHasher<User> _passwordHasher;

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordService"/> class.
        /// </summary>
        public PasswordService()
        {
            _passwordHasher = new PasswordHasher<User>();
        }

        /// <summary>
        /// Hashes a password for a user.
        /// </summary>
        /// <param name="user">The user whose password is being hashed.</param>
        /// <param name="password">The plain-text password to hash.</param>
        /// <returns>A hashed version of the password.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the user or password is null.</exception>
        public string HashPassword(User user, string password)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");
            }

            return _passwordHasher.HashPassword(user, password);
        }

        /// <summary>
        /// Verifies if the provided password matches the hashed password of the user.
        /// </summary>
        /// <param name="user">The user whose password needs to be verified.</param>
        /// <param name="password">The plain-text password to verify.</param>
        /// <returns>True if the password is correct, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the user or password is null.</exception>
        public bool VerifyPassword(User user, string password)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
