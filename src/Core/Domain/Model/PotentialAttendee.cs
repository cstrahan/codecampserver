using Tarantino.Core.Commons.Model;

namespace CodeCampServer.Core.Domain.Model
{
    public class PotentialAttendee : PersistentObject
    {
        public virtual string Name { get; set; }
        public virtual string EmailAddress { get; set; }
    }
}