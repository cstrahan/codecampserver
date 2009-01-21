using AutoMapper;

namespace CodeCampServer.UI
{
	public class AutoMapperConfiguration
	{
		public static void Configure()
		{
			Mapper.Initalize(x => x.AddProfile<CodeCampServerProfile>());
		}
	}
}