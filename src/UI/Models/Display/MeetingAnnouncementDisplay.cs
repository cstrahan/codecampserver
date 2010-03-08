using System;
using CodeCampServer.Core.Bases;
using CodeCampServer.Core.Domain.Model;
using CodeCampServer.UI.Models.Input;
using MvcContrib.UI.InputBuilder.Attributes;

namespace CodeCampServer.UI.Models.Display
{
    public class MeetingAnnouncementDisplay : IKeyable, IGloballyUnique
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Heading { get; set; }
        public string Topic { get; set; }
        public string Summary { get; set; }
        public DateTimeSpan When { get; set; }
        public string LocationName { get; set; }
        public string LocationUrl { get; set; }

        [Label("Speaker")]
        public string SpeakerName { get; set; }

        public string SpeakerUrl { get; set; }
        public string SpeakerBio { get; set; }
        public string MeetingInfo { get; set; }
    }
}