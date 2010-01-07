using System;

namespace CodeCampServer.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void Begin();
        void Commit();
        void RollBack();
    }
}