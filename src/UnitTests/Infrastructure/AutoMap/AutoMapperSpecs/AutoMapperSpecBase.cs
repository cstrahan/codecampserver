using CodeCampServer.Infrastructure.AutoMap;
using NBehave.Spec.NUnit;

namespace CodeCampServer.UnitTests.Infrastructure.AutoMap.AutoMapperSpecs
{
	public class AutoMapperSpecBase : SpecBase
	{
		protected override void Cleanup()
		{
			AutoMapper.Reset();
		}
	}
}