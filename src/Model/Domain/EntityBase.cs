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
            return Equals(obj as EntityBase);
        }

        public override int GetHashCode()
        {
            return _id != Guid.Empty ? _id.GetHashCode() : base.GetHashCode();
        }

        public bool Equals(EntityBase other)
        {
            if (other == null) return false;
            if (_id == Guid.Empty) return base.Equals(other);
            return Equals(_id, other.Id);
        }
    }
}