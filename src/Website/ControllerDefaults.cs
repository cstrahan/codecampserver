namespace CodeCampServer.Website
{
    public class ControllerDefaults
    {
        private readonly string _defaultAction;
        private readonly string _controller;

        public ControllerDefaults(string defaultAction)
        {
            _defaultAction = defaultAction;
        }

        public ControllerDefaults(string defaultAction, string controller)
        {
            _defaultAction = defaultAction;
            _controller = controller;
        }

        public string action
        {
            get { return _defaultAction; }
        }

        public string controller
        {
            get { return _controller; }
        }
    }
}