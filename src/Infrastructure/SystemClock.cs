using System;
using CodeCampServer.Core.Services.Bases;


namespace CodeCampServer.Infrastructure
{
    public class SystemClock : ISystemClock
    {
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}