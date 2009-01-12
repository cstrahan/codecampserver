using CodeCampServer.Infrastructure.AutoMap;

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