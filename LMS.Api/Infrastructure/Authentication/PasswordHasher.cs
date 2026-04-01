using LMS.Api.Application.Utilities;
using System.Security.Cryptography;
using System.Text;

namespace LMS.Api.Infrastructure.Authentication;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }

    public bool VerifyPassword(string inputPassword, string storedHash)
    {
        string inputHash = HashPassword(inputPassword);
        return inputHash == storedHash;
    }
}
