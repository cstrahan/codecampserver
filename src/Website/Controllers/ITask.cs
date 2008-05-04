namespace CodeCampServer.Website.Controllers
{
    public interface ITask
    {
        void Execute();
        bool Success { get; }
        string ErrorMessage { get;}
    }
}