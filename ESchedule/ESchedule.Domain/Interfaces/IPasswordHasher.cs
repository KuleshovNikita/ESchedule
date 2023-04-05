namespace ESchedule.Core.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);

        bool ComparePasswords(string actualAsValue, string expectedAsHash);
    }
}
