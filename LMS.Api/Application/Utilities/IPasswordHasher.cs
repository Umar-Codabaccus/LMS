namespace LMS.Api.Application.Utilities
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string inputPassword, string storedHash);
    }
}