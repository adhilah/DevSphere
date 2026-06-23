namespace DevSphere.Application.Interfaces.Authentication
{
    public interface IPasswordHasher
    {
        String HashPassword(String password);
        bool VerifyHashedPassword(String password, String hashedPassword);
    }
}