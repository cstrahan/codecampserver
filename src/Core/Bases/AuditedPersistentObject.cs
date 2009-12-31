using System;
using CodeCampServer.Core.Bases;

namespace CodeCampServer.Core.Domain.Model
{
	public abstract class AuditedPersistentObjectOfGuid : AuditedPersistentObject
	{
		public new virtual Guid Id
		{
			get { return (Guid) base.Id; }
			set { base.Id = value; }
		}

		protected override object GetEmptyId()
		{
			return default(Guid);
		}
	}

	public abstract class AuditedPersistentObjectOfInt32 : AuditedPersistentObject
	{
		public new virtual Int32 Id
		{
			get { return (int) base.Id; }
			set { base.Id = value; }
		}

		protected override object GetEmptyId()
		{
			return default(Int32);
		}
	}

	public abstract class AuditedPersistentObject : PersistentObject, IAuditable
	{
		private ChangeAuditInfo _changeAuditInfo;

		protected AuditedPersistentObject()
		{
			_changeAuditInfo = new ChangeAuditInfo();
		}

		public virtual ChangeAuditInfo ChangeAuditInfo
		{
			get
			{
				if (_changeAuditInfo == null)
					_changeAuditInfo = new ChangeAuditInfo();

				return _changeAuditInfo;
			}
			set
			{
				if (value == null)
					return;

				_changeAuditInfo = value;
			}
		}
	}
}