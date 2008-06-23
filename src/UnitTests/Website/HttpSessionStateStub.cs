using System.Collections;
using System.Web;

namespace CodeCampServer.UnitTests.Website
{
	public class HttpSessionStateStub : HttpSessionStateBase
	{
		private readonly Hashtable _state = new Hashtable();

		public override object this[string name]
		{
			get
			{
				if (!_state.ContainsKey(name))
				{
					return null;
				}

				return _state[name];
			}
			set { _state[name] = value; }
		}
	}
}