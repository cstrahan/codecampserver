using Tarantino.Core.Commons.Model.Enumerations;

namespace CodeCampServer.Core.Domain.Model
{
    public class AttendanceStatus:Enumeration
    {
        public AttendanceStatus()
        {
            
        }

        public AttendanceStatus(string displayName,int value):base(value,displayName)
        {
            
        }
        public static AttendanceStatus NotAttending = new AttendanceStatus("Not Attending",1);
        public static AttendanceStatus Attending = new AttendanceStatus("Attending",2);
        public static AttendanceStatus Confirmed = new AttendanceStatus("Confirmed", 3);
    }
}