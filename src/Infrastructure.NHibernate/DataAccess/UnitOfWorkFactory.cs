using System;
using CodeCampServer.Core;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess
{
    public class UnitOfWorkFactory : AbstractFactoryBase<IUnitOfWork>
    {
        public static Func<IUnitOfWork> Default = DefaultUnconfiguredState;
    }
}