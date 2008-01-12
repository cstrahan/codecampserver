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
using CodeCampServer.Model.Domain;
using System.Collections.Generic;
using CodeCampServer.Model.Presentation;

namespace CodeCampServer.Website.Models.Conference
{
    public class ListAttendeesViewData : IEquatable<ListAttendeesViewData>
    {
        private ScheduledConference _conference;
        private IEnumerable<AttendeeListing> _attendees;

        public ListAttendeesViewData()
        {
        }

        public ListAttendeesViewData(ScheduledConference conference, IEnumerable<AttendeeListing> attendees)
        {
            _conference = conference;
            _attendees = attendees;
        }

        public ScheduledConference Conference
        {
            get { return _conference; }
            set { _conference = value; }
        }

        public IEnumerable<AttendeeListing> Attendees
        {
            get { return _attendees; }
            set { _attendees = value; }
        }


        public bool Equals(ListAttendeesViewData listAttendeesViewData)
        {
            if (listAttendeesViewData == null) return false;
            return Equals(_conference, listAttendeesViewData._conference) && Equals(_attendees, listAttendeesViewData._attendees);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as ListAttendeesViewData);
        }

        public override int GetHashCode()
        {
            return _conference.GetHashCode() + 29*_attendees.GetHashCode();
        }
    }
}
