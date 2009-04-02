namespace CodeCampServer.UI.Models
{
	public class PageInfo
	{
		public string Title { get; set; }
        public string TrackingCode { get; set; }
	    public string MasterTrackingCode
	    {
	        get
	        {
	            return "UA-7326641-2";
	        }
	    }
	    public bool TrackingCodeExists
	    {
	        get
	        {
                return TrackingCode!=null && TrackingCode.Length > 0;
	        }
	    }
	}
}