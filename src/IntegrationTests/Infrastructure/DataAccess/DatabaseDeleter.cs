using System;
using System.Linq;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Infrastructure.NHibernate.DataAccess;

namespace CodeCampServer.IntegrationTests.Infrastructure.DataAccess
{
	public class DatabaseDeleter
	{
		private readonly IUnitOfWork _unitOfWork;

		public DatabaseDeleter(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		internal virtual void DeleteAllObjects()
		{
			Type[] types =
				typeof (User).Assembly.GetTypes().Where(
					type => typeof (PersistentObject).IsAssignableFrom(type) && !type.IsAbstract)
					.OrderBy(type => type.Name).ToArray();

			foreach (var type in types)
			{
                _unitOfWork.CurrentSession.CreateQuery(string.Format("delete {0}", type.Name)).ExecuteUpdate();
			}
			_unitOfWork.Commit();
		}
	}
}