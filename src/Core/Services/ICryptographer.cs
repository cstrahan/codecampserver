namespace CodeCampServer.Core.Services
{
	public interface ICryptographer
	{
		string CreateSalt();
		string ComputeHash(string valueToHash);
	}
}