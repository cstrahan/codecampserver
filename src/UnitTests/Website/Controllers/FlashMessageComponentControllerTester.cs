using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CodeCampServer.Model;
using CodeCampServer.Website.Controllers;
using MvcContrib;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace CodeCampServer.UnitTests.Website.Controllers
{
	[TestFixture]
	public class FlashMessageComponentControllerTester
	{
		[Test]
		public void ShouldGetFlashMessages()
		{
			var messagesToReturn = new[]
			                       	{
			                       		new FlashMessage(FlashMessage.MessageType.Error, ""),
			                       		new FlashMessage(FlashMessage.MessageType.Message, "")
			                       	};

			var userSession = MockRepository.GenerateStub<IUserSession>();
			userSession.Stub(s => s.PopUserMessages()).Return(messagesToReturn);

			var component = new FlashMessageComponentController(userSession);
			var result = component.GetMessages() as ViewResult;
			var actualMessages = result.ViewData.Model as FlashMessage[];

			Assert.That(result.ViewName, Is.EqualTo("FlashMessageList"));
			Assert.That(actualMessages.Length, Is.EqualTo(2));
			Assert.That(actualMessages[0].Type, Is.EqualTo(FlashMessage.MessageType.Error));
			Assert.That(actualMessages[1].Type, Is.EqualTo(FlashMessage.MessageType.Message));
		}
	}
}