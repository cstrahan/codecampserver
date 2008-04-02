namespace CodeCampServer.Model
{
    public interface ICryptoUtil
    {
        string CreateSalt();
        string HashPassword(string pwd, string salt);
    }
}