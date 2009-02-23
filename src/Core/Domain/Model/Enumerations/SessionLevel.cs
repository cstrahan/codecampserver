using Tarantino.Core.Commons.Model.Enumerations;

namespace CodeCampServer.Core.Domain.Model.Enumerations
{
	public class SessionLevel : Enumeration
	{
		public static SessionLevel L100 = new SessionLevel(1, "100");
		public static SessionLevel L200 = new SessionLevel(2, "200");
		public static SessionLevel L300 = new SessionLevel(3, "300");
		public static SessionLevel L400 = new SessionLevel(4, "400");

		public SessionLevel()
		{
			
		}

		public SessionLevel(int value, string displayName) : base(value, displayName)
		{
		}
	}
}