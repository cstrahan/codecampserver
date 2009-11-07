using System;

namespace CodeCampServer.Core.Bases
{
	public abstract class PersistentObject
	{
		public const string ID = "Id";

		public virtual Guid Id { get; set; }

		public virtual bool IsPersistent
		{
			get { return isPersistentObject(); }
		}

		public override bool Equals(object obj)
		{
			if (isPersistentObject())
			{
				var persistentObject = obj as PersistentObject;
				return (persistentObject != null) && (Id == persistentObject.Id);
			}

			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return isPersistentObject() ? Id.GetHashCode() : base.GetHashCode();
		}

		private bool isPersistentObject()
		{
			return (Id != Guid.Empty);
		}
	}
}