using System.Linq;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Common;
using CodeCampServer.Core.Services.Unique;
using NHibernate;
using NHibernate.Criterion;

namespace CodeCampServer.Infrastructure.NHibernate
{
	public class EntityCounter : IEntityCounter
	{
		private readonly DataAccess.ISessionBuilder _sessionBuilder;

		public EntityCounter(DataAccess.ISessionBuilder sessionBuilder)
		{
			_sessionBuilder = sessionBuilder;
		}

		private ISession GetSession()
		{
			return _sessionBuilder.GetSession();
		}

		public int CountByProperty<TModel>(IEntitySpecification<TModel> specification) where TModel : PersistentObject
		{
			var property = UINameHelper.BuildNameFrom(specification.PropertyExpression);

			var criteria = GetSession().CreateCriteria(typeof (TModel));

			criteria.Add(Restrictions.Eq(property, specification.Value));

			if (specification.HasExistingId)
				criteria.Add(Restrictions.Not(Restrictions.Eq(PersistentObject.ID, specification.ExistingId)));

			return criteria.List<TModel>().Count();
		}
	}
}