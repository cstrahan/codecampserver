using System;
using CodeCampServer.UI;
using CodeCampServer.UI.Controllers;
using CodeCampServer.UI.Models.Input;
using MvcContrib.TestHelper.Ui;
using NUnit.Framework;

namespace CodeCampServerUiTests
{
	[TestFixture]
	public class UserGroupEditViewTester : UiTestBase
	{
		[Test]
		public void Should_Create_New_User_Groups()
		{
			var key = Guid.NewGuid().ToString();
			var groupName = Guid.NewGuid().ToString();
			_webBrowser.ScreenCaptureOnFailure(() =>
               	{
               		AddNewUserGroup(key, groupName);

               		var table = new DisplayTable<UserGroupInput>(_webBrowser);
               		table.AddRowFilter(u => u.Name, groupName);

               		Assert.IsTrue(table.VerifyRowExists());

               		table.ClickLink(CodeCampSite.Admin.ViewUserGroup);

					_webBrowser.VerifyPage<UserGroupController>(p => p.Index(null));
               	});
		}

		[Test]
		public void Should_require_fields()
		{
			_webBrowser.ScreenCaptureOnFailure(() =>
			{
				var groupName = Guid.NewGuid().ToString();
				GoToUserGroupAdminPage();

				_webBrowser.ClickLink(CodeCampSite.Admin.CreateUserGroup);
				var form = new InputForm<UserGroupInput>(_webBrowser);
				form
					.Input(u => u.Name, groupName)
					.Submit();

				_webBrowser.VerifyPage<UserGroupController>(p => p.Edit((UserGroupInput)null));

				_webBrowser.ValidationSummaryExists();
				_webBrowser.ValidationSummaryContainsMessageFor<UserGroupInput>(
					m => m.Key);
				_webBrowser.AssertValue<UserGroupInput>(m => m.Name, groupName);
			});
		}

		[Test]
		public void Should_edit_user_group()
		{
			var key = Guid.NewGuid().ToString();
			var groupName = Guid.NewGuid().ToString();
			var groupName2 = Guid.NewGuid().ToString();
			_webBrowser.ScreenCaptureOnFailure(() =>
			{
				AddNewUserGroup(key, groupName);

				var table = new DisplayTable<UserGroupInput>(_webBrowser);
				table.AddRowFilter(u => u.Name, groupName);

				Assert.IsTrue(table.VerifyRowExists());

				table.ClickLink(CodeCampSite.Admin.EditUserGroup);
				_webBrowser.VerifyPage<UserGroupController>(p => p.Edit((UserGroupInput)null));

				var form = new InputForm<UserGroupInput>(_webBrowser);
				form
					.Input(u => u.Name, groupName2)
					.Submit();

				var table2 = new DisplayTable<UserGroupInput>(_webBrowser);
				table2.AddRowFilter(u => u.Name, groupName2);
				table2.AddRowFilter(u => u.Key, key);

				Assert.IsTrue(table2.VerifyRowExists());
			});
		}


		private void AddNewUserGroup(string key, string groupName) {
			GoToUserGroupAdminPage();

			_webBrowser.ClickLink(CodeCampSite.Admin.CreateUserGroup);

			var form = new InputForm<UserGroupInput>(_webBrowser);
			form
				.Input(u => u.Key, key)
				.Input(u => u.Name, groupName)
				.Input(u => u.Users, "Joe User", "Bart Simpson")
				.Submit();

			_webBrowser.VerifyPage<UserGroupController>(p => p.List());
		}

		private void GoToUserGroupAdminPage() {
			Form<LoginProxyInput>("/login/login/index")
				.Input(m => m.Username, "admin")
				.Input(m => m.Password, "password")
				.Submit();
			_webBrowser.VerifyPage<HomeController>(p => p.Index(null));

			_webBrowser.ClickLink(CodeCampSite.Navigation.Admin);
			_webBrowser.VerifyPage<AdminController>(p => p.Index(null));

			_webBrowser.ClickLink(CodeCampSite.Admin.EditUserGroups);
			_webBrowser.VerifyPage<UserGroupController>(p => p.List());
		}
	}
}