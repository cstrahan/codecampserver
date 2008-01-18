using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using CodeCampServer.Model.Presentation;

namespace CodeCampServer.Website.Models.Speaker
{
    public class ListSpeakersViewData
    {
        private ScheduledConference _conference;
        private IEnumerable<SpeakerListing> _speakers;
        private int _page;
        private int _perPage;

        public ListSpeakersViewData(ScheduledConference conference, IEnumerable<SpeakerListing> speakers, int page, int perPage)
        {
            _conference = conference;
            _speakers = speakers;
            _page = page;
            _perPage = perPage;
        }

        public ScheduledConference Conference
        {
            get { return _conference; }
            set { _conference = value; }
        }

        public IEnumerable<SpeakerListing> Speakers
        {
            get { return _speakers; }
            set { _speakers = value; }
        }

        public int Page
        {
            get { return _page; }
            set { _page = value; }
        }

        public int PerPage
        {
            get { return _perPage; }
            set { _perPage = value; }
        }
    }
}
