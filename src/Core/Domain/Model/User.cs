using Tarantino.Core.Commons.Model;

namespace CodeCampServer.Core.Domain.Model
{
    public class User : PersistentObject
    {
        public virtual string Username { get; set; }
        public virtual string Name { get; set; }
        public virtual string EmailAddress { get; set; }
    }
}