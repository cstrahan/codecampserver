using System;
using CodeCampServer.Core;

namespace CodeCampServer.Infrastructure.DataAccess
{
    public class UnitOfWorkFactory:StaticFactory<IUnitOfWork>
    {
        public static Func<IUnitOfWork> Default = DefaultUnconfiguredState;        
    }
}