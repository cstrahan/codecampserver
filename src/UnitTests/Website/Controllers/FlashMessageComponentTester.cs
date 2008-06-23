using System;
using CodeCampServer.Model;
using CodeCampServer.Website.Controllers;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
	[TestFixture]
	public class FlashMessageComponentTester
	{
		[Test]
		public void ShouldGetFlashMessages()
		{
			string viewName = null;
			FlashMessage[] actualMessages = null;
			var messagesToReturn = new[]
			                       	{
			                       		new FlashMessage(FlashMessage.MessageType.Error, ""),
			                       		new FlashMessage(FlashMessage.MessageType.Message, "")
			                       	};

			var userSession = MockRepository.GenerateStub<IUserSession>();
			userSession.Stub(s => s.PopUserMessages()).Return(messagesToReturn);

			var component = MockRepository.GenerateStub<FlashMessageComponent>(userSession);

			component.Stub(c => c.RenderView(null, null)).IgnoreArguments()
				.Do(new Action<string, object>((x, y) =>
				                               	{
				                               		viewName = x;
				                               		actualMessages = (FlashMessage[]) y;
				                               	}));

			component.GetMessages();

			Assert.That(viewName, Is.EqualTo("list"));
			Assert.That(actualMessages.Length, Is.EqualTo(2));
			Assert.That(actualMessages[0].Type, Is.EqualTo(FlashMessage.MessageType.Error));
			Assert.That(actualMessages[1].Type, Is.EqualTo(FlashMessage.MessageType.Message));
		}
	}
}