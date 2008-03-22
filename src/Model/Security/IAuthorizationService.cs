namespace CodeCampServer.Model.Security
{
    public interface IAuthorizationService
    {
        bool IsAdministrator { get; }
    }
}
