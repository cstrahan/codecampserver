namespace CodeCampServer.Model.Domain
{
    public class OnlineResource
    {
        private OnlineResourceType _type;
        private string _name;
        private string _href;

        public OnlineResource()
        {
        }
        public OnlineResource(OnlineResourceType resourceType, string name, string href)
        {
            _type = resourceType;
            _name = name;
            _href = href;
        }

        public virtual OnlineResourceType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual string Href
        {
            get { return _href; }
            set { _href = value; }
        }
    }
}
