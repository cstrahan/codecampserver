namespace CodeCampServer.Core.Bases
{
	public abstract class PersistentObject
	{
		public const string ID = "Id";

		public virtual object Id { get; set; }

		protected PersistentObject()
		{
			Id = GetEmptyId();
		}

		public virtual bool IsPersistent
		{
			get { return isPersistentObject(); }
		}

		public override bool Equals(object obj)
		{
			if (isPersistentObject())
			{
				var persistentObject = obj as PersistentObject;
				return (persistentObject != null) && (IdsAreEqual(persistentObject));
			}

			return base.Equals(obj);
		}

		protected bool IdsAreEqual(PersistentObject persistentObject)
		{
			return Equals(Id, persistentObject.Id);
		}

		public override int GetHashCode()
		{
			return isPersistentObject() ? Id.GetHashCode() : base.GetHashCode();
		}

		private bool isPersistentObject()
		{
			return !Equals(Id, GetEmptyId());
		}

		protected abstract object GetEmptyId();
	}
}