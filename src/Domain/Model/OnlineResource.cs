namespace CodeCampServer.Domain.Model
{
    public class OnlineResource
    {
        public enum OnlineResourceType
        {
            Download,
            Website,
            Blog,
            Other
        }

        private OnlineResourceType _type;
        private string _name;
        private string _href;

        public OnlineResourceType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Href
        {
            get { return _href; }
            set { _href = value; }
        }
    }
}