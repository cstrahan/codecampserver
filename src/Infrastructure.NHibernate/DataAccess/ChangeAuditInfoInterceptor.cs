using System;
using CodeCampServer.Core;
using CodeCampServer.Core.Common;
using CodeCampServer.Core.Domain;
using CodeCampServer.Core.Domain.Bases;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.Core.Services;
using NHibernate;
using NHibernate.Type;

namespace CodeCampServer.Infrastructure.NHibernate.DataAccess
{
	public class ChangeAuditInfoInterceptor : EmptyInterceptor
	{
		private readonly IUserSession _userSession;
		private readonly ISystemClock _clock;

		public static bool AuditingEnabled = true;

		public ChangeAuditInfoInterceptor(IUserSession userSession, ISystemClock clock)
		{
			_userSession = userSession;
			_clock = clock;
		}

		public override bool OnFlushDirty(object entity, object id, object[] currentState, object[] previousState,
		                                  string[] propertyNames, IType[] types)
		{
			if (!AuditingEnabled)
				return false;

			string propertyName = UINameHelper.BuildNameFrom<IAuditable>(x => x.ChangeAuditInfo);

			if (entity is IAuditable && !(entity is User))
			{
				var auditable = (IAuditable) entity;

				Action<object> setter = GetSetter(propertyNames, currentState, propertyName);
				CommonAudit(auditable, setter);
				return true;
			}
			return false;
		}

		public override bool OnSave(object entity, object id, object[] state, string[] propertyNames, IType[] types)
		{
			if (!AuditingEnabled)
				return false;

			string propertyName = UINameHelper.BuildNameFrom<IAuditable>(x => x.ChangeAuditInfo);

			if (entity is IAuditable && !(entity is User))
			{
				var auditable = (IAuditable) entity;

				Action<object> setter = GetSetter(propertyNames, state, propertyName);
				CommonAudit(auditable, setter);
				return true;
			}
			return false;
		}

		private void CommonAudit(IAuditable auditable, Action<object> setter)
		{
			if (auditable.ChangeAuditInfo.Created == null) //create
			{
				auditable.ChangeAuditInfo.Created = _clock.Now();
				auditable.ChangeAuditInfo.CreatedBy = _userSession.GetCurrentUser().Username;
			}
			auditable.ChangeAuditInfo.Updated = _clock.Now();
			auditable.ChangeAuditInfo.UpdatedBy = _userSession.GetCurrentUser().Username;


			setter(auditable.ChangeAuditInfo);
		}

		private static Action<object> GetSetter(string[] propertyNames, object[] currentState, string propertyName)
		{
			int index = Array.IndexOf(propertyNames, propertyName);
			if (index < 0 || index >= propertyNames.Length)
				return x => { };
			return value => currentState[index] = value;
		}
	}
}