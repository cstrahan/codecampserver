using System;

namespace CodeCampServer.UI.Models.Forms
{
    public class AttendeeForm
    {
        public Guid ConferenceID { get; set; }
        public string Email { get; set; }
        public string Name{ get; set;}
    }
}