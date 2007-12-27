using System;

namespace CodeCampServer.Model.Domain
{
    public class EntityBase : IEquatable<EntityBase>
    {
        private Guid _id;

        public virtual Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as Conference);
        }

        public override int GetHashCode()
        {
            return Id != Guid.Empty ? Id.GetHashCode() : base.GetHashCode();
        }

        public bool Equals(EntityBase other)
        {
            if (other == null) return false;
            if (Id == Guid.Empty) return base.Equals(other);
            return Equals(Id, other.Id);
        }
    }
}