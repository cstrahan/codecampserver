using System;
using System.Linq;
using System.Linq.Expressions;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Common;
using CodeCampServer.Core.Services;
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

		public int CountByProperty<TModel>(Expression<Func<TModel, object>> propertyExpression, object value)
			where TModel : PersistentObject
		{
			var property = UINameHelper.BuildNameFrom(propertyExpression);
			return GetSession()
				.CreateCriteria(typeof (TModel))
				.Add(Restrictions.Eq(property, value))
				.List<TModel>()
				.Count();
		}
	}
}