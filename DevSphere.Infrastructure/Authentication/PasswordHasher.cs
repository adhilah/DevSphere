using BCrypt;
using DevSphere.Application.Interfaces.Authentication;

namespace DevSphere.Infrastructure.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        public String HashPassword(String password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyHashedPassword(String password, String hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}

