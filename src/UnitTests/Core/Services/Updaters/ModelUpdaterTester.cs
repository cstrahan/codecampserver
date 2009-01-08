using CodeCampServer.Core.Services.Updaters.Impl;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Tarantino.Core.Commons.Model;

namespace CodeCampServer.UnitTests.Core.Services.Updaters
{
	[TestFixture]
	public class ModelUpdaterTester : TestBase
	{
		private class Model : PersistentObject
		{
		}

		private class Message
		{
			public int Foo { get; set; }
		}

		[Test]
		public void Success_is_successful()
		{
			ModelUpdater<Model, Message>.Success().Successful.ShouldBeTrue();
		}

		[Test]
		public void Fail_should_not_be_successful()
		{
			ModelUpdater<Model, Message>.Fail().Successful.ShouldBeFalse();
		}

		[Test]
		public void With_message_should_add_message()
		{
			ModelUpdater<Model, Message>.Fail().WithMessage(x => x.Foo, "bar").GetMessages("Foo").ShouldEqual(new[]{"bar"});
		}

		[Test]
		public void With_message_again_should_add_another_message()
		{
			ModelUpdater<Model, Message>.Fail().WithMessage(x => x.Foo, "bar").WithMessage(x => x.Foo, "baz").GetMessages("Foo").ShouldEqual(new[] { "bar", "baz" });
		}
	}
}