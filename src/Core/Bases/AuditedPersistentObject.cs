using System;
using CodeCampServer.Core.Domain.Bases;

namespace CodeCampServer.Core.Bases
{
	public abstract class AuditedPersistentObjectOfGuid : AuditedPersistentObject, IPersistentObjectOfGuid
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

		public static Guid EmptyId = default(Guid);
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

	public abstract class PersistentObjectOfGuid : PersistentObject, IPersistentObjectOfGuid
	{
		public new virtual Guid Id
		{
			get { return (Guid)base.Id; }
			set { base.Id = value; }
		}

		protected override object GetEmptyId()
		{
			return default(Guid);
		}

		public static Guid EmptyId = default(Guid);
	}

	public interface IPersistentObjectOfGuid
	{
		Guid Id { get; set; }
	}
}