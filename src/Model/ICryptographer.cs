namespace CodeCampServer.Model
{
    public interface ICryptographer
    {
        string CreateSalt();
        string HashPassword(string pwd, string salt);
    }
}