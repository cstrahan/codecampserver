using CodeCampServer.Infrastructure.AutoMap;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.Infrastructure.AutoMap.AutoMapperSpecs
{
	[TestFixture]
	public class AutoMapperTester
	{
		[Test]
		public void Should_be_able_to_handle_derived_proxy_types()
		{
			AutoMapper.CreateMap<ModelType, DtoType>();
			var source = new[] {new DerivedModelType {TheProperty = "Foo"}, new DerivedModelType {TheProperty = "Bar"}};

			var destination = (DtoType[]) AutoMapper.Map(source, typeof (ModelType[]), typeof (DtoType[]));

			destination[0].TheProperty.ShouldEqual("Foo");
			destination[1].TheProperty.ShouldEqual("Bar");
		}

		[TearDown]
		public void Teardown()
		{
			AutoMapper.Reset();
		}

		private class ModelType
		{
			public string TheProperty { get; set; }
		}

		private class DerivedModelType : ModelType
		{
		}

		private class DtoType
		{
			public string TheProperty { get; set; }
		}
	}
}