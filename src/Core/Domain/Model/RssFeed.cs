namespace CodeCampServer.Core.Domain.Model
{
    public class RssFeed : PersistentObject
    {
        public virtual string Name { get; set; }
        public virtual string Url { get; set; }
    }
}