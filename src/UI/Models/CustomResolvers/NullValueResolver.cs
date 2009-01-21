using AutoMapper;

namespace CodeCampServer.UI.Models.CustomResolvers
{
	public class NullValueResolver : IValueResolver
	{
		#region IValueResolver Members

		public object Resolve(object model)
		{
			return null;
		}

		#endregion
	}
}