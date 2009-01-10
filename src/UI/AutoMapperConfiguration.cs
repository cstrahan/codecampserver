using CodeCampServer.Infrastructure.AutoMap;
using CodeCampServer.UI;

namespace CodeCampServer.UI
{
	public class AutoMapperConfiguration
	{
		public static void Configure()
		{
			AutoMapper.Initalize(x => x.AddProfile<CodeCampServerProfile>());
		}
	}
}