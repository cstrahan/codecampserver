using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading;
using CodeCampServer.Core.Common;
using CodeCampServer.RegressionTests.TestHelpers.SmartWatiN;
using Gallio.Framework;
using Gallio.Model;
using Gallio.Model.Logging;
using MbUnit.Framework;
using mshtml;
using RegressionTests;
using RegressionTests.Web;
using WatiN.Core;
using AttributeConstraint=WatiN.Core.Constraints.AttributeConstraint;

namespace CodeCampServer.RegressionTests.Web
{
	[Parallelizable]
	[ApartmentState(ApartmentState.STA)]
	public class WebTest
	{
		private static readonly LocalDataStoreSlot DATA_SLOT =
			Thread.AllocateDataSlot();

		[ThreadStatic] protected static IE ie;

		protected virtual bool AutoLogin
		{
			get { return true; }
		}

		protected virtual string UserName
		{
			get { return "system"; }
		}

		protected virtual string Password
		{
			get { return UserName; }
		}

		public static WebTest CurrentTest()
		{
			return Thread.GetData(DATA_SLOT) as WebTest;
		}

		[FixtureSetUp]
		public virtual void FixtureSetup()
		{
		}

		[SetUp]
		public virtual void Setup()
		{
			try
			{
				ie = IEFactory.GetInternetExplorer(); // new SmartIE(true);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				ExecutionSteps.Log.WriteException(e);
				if (ie != null)
				{
					IEFactory.Release(ie);
				}
				throw;
			}

			Thread.SetData(DATA_SLOT, this);

			LoadSite();

			if (AutoLogin)
			{
				LogoutIfLoggedIn();
				LoginAs(UserName, Password);
			}
		}

		private static void LoadSite()
		{
			ie.GoTo(UIConstants.BASE_URL + "/login/logout");
			if (ie.Html.Contains("cannot display the webpage"))
				Assert.Fail("Server is not running.");
		}

		public string ErrorContext()
		{
			string result = "The current user is '" + UserName;

			if (ie != null)
				result += "'\r\nThe current URL is " + ie.Url;

			return result;
		}

		[FixtureTearDown]
		public virtual void FixtureTearDown()
		{
		}


		[TearDown]
		public virtual void TearDown()
		{
			try
			{
				bool testError = TestContext.CurrentContext.Outcome.Status ==
				                 TestStatus.Failed;
				bool serverError = ie.ContainsText("Server Error");

				if (testError || serverError)
				{
					Snapshot("Test Failed on this page", ExecutionSteps.Log);
					Assert.IsFalse(serverError,
					               "An error left the system in a failing state.\n" +
					               ErrorContext());
				}
				LogoutIfLoggedIn();
				ie.GoTo("about:blank");
			}
			finally
			{
				IEFactory.Release(ie);
				ie = null;
				Thread.SetData(DATA_SLOT, null);
			}
		}

		private static void Snapshot(string caption, TestLogStreamWriter logStreamWriter)
		{
			using (logStreamWriter.BeginSection(caption))
			{
				logStreamWriter.Write("Url: ");
				using (logStreamWriter.BeginMarker(Marker.Link(ie.Url)))
					logStreamWriter.WriteLine(ie.Url);
				logStreamWriter.EmbedImage(caption + ".png",
				                           new CaptureWebPage(ie).CaptureWebPageImage(
				                           	false, false, 100));
			}
		}

		protected virtual void LoginAs(string userName, string password)
		{
			ExecutionSteps.Log.WriteLine(string.Format("Log in as '{0}'", userName));

			// enter login info
			ie.TextField(Find.ById("username")).Value = userName;
			ie.TextField(Find.ById("password")).Value = password;

			// submit
			ie.Button("login").Click();
		}

		public virtual void Logout()
		{
			ExecutionSteps.Log.WriteLine("Log out");
			ie.Link("logout").Click();
			// todo convert to form
		}

		public virtual void LogoutIfLoggedIn()
		{
			LoadSite();
			Link link = ie.Link("logout");
			if (link.Exists)
			{
				ExecutionSteps.Log.WriteLine("Log out");
				try
				{
					link.ClickNoWait();
				}
				catch
				{
				}
			}
		}

		public virtual SmartCheckBox InCheckBox(string id)
		{
			return new SmartCheckBox(ie.CheckBox("countyId"));
		}

		public virtual SmartTextField InTextField(string id)
		{
			return new SmartTextField(ie.TextField(id));
		}

		public virtual void GoToPage(string pageLinkId)
		{
			Link link = ie.Link(pageLinkId);
			link.Click();
		}

		protected void GoBack()
		{
			ExecutionSteps.Log.WriteLine("Go back a page");
			ie.Back();
		}

		protected void Click(string controlId)
		{
			ie.Button(controlId).Click();
		}

		protected void AssertHas(string identifier)
		{
			Element element = ie.Element(identifier);
			Assert.IsNotNull(element);
		}

		protected static void AssertNotVisible(Element element)
		{
			var htmlElement = (IHTMLElement2) element.HTMLElement;
			string display = htmlElement.currentStyle.display;

			Assert.AreEqual("none", display);
		}

		protected static void AssertVisible(Element element)
		{
			var htmlElement = (IHTMLElement2) element.HTMLElement;
			string display = htmlElement.currentStyle.display;

			Assert.AreNotEqual("none", display);
		}

		public virtual void GoToSearchPage()
		{
			ie.Link(Find.ByUrl(ToUrl("/search"))).Click();
		}

		private static string ToUrl(string relativeUrl)
		{
			return UIConstants.BASE_URL + relativeUrl;
		}

		protected void Follow(string transition)
		{
			Follow(transition, null, null);
		}

		protected void Follow(string transition,
		                      IDictionary<string, object> withParams, int countDown)
		{
			Follow(transition, withParams, null, countDown);
		}

		protected void Follow(string transition, Until until)
		{
			Follow(transition, null, until);
		}

		protected void Follow(string transition,
		                      IDictionary<string, object> withParams, Until until)
		{
			ExecutionSteps.Log.WriteLine("Navigate to '{0}'", transition);
			if (withParams != null)
			{
				ExecutionSteps.Log.WriteLine(
					"Perform '{0}' action with the following parameters:", transition);
				foreach (var pair in withParams)
				{
					ExecutionSteps.Log.WriteLine("    {0} = '{1}'", pair.Key, pair.Value);
				}
			}
			Follow(transition, withParams, until, 4);
		}

		protected void Follow(string transition,
		                      IDictionary<string, object> withParams, Until until,
		                      int countDown)
		{
			string after = null;
			if (until != null)
				after = " looking for: " + until.s;

			Assert.IsTrue(countDown > 0,
			              "Didn't match transition:" + transition + after + "\n" +
			              ErrorContext());

			Element elem = LocateLink(transition);

			Assert.IsNotNull(elem,
			                 "Can't find element for transition: " + transition + "\n" +
			                 ErrorContext());

			if (withParams != null)
				ApplyParams(elem, withParams);

			Follow(elem);

			if (until == null || Locate(until.s, false) != null)
				return;

			Follow(transition, withParams, until, countDown - 1);
		}

		private static void ApplyParams(Element link,
		                                IDictionary<string, object> withParams)
		{
			if (link is Form)
			{
				var form = (Form) link;

				foreach (TextField textfield in form.TextFields)
				{
					string name = textfield.Name;
					if (withParams.ContainsKey(name))
					{
						string value = withParams[name].ToString();
						textfield.Value = value;
						MaybeWaitFor(textfield);
					}
				}

				foreach (CheckBox checkbox in form.CheckBoxes)
				{
					string name = GetName(checkbox);
					if (withParams.ContainsKey(name))
					{
						var value = (bool) withParams[name];
						checkbox.Checked = value;
						MaybeWaitFor(checkbox);
					}
				}

				foreach (SelectList selectList in form.SelectLists)
				{
					string name = GetName(selectList);
					if (withParams.ContainsKey(name))
					{
						var value = (string) withParams[name];
						selectList.Select(value);
						MaybeWaitFor(selectList);
					}
				}
			}
		}

		public static void MaybeWaitFor(Element element)
		{
			string className = element.ClassName;

			if (className == null)
				return;

			foreach (string name in className.Split(' '))
			{
				string waitfor = "waitfor";
				if (name.StartsWith(waitfor))
				{
					string id = name.Substring(waitfor.Length);
					Element waitOn = element.DomContainer.Element(id);
					if (waitOn.Exists)
					{
						waitOn.WaitUntil(Find.By("disabled", "False"));
					}
				}
			}
		}

		private static string GetName(Element element)
		{
			object htmlElement = element.HTMLElement;
			if (htmlElement is IHTMLInputElement)
			{
				var inputElement = (IHTMLInputElement) htmlElement;
				return inputElement.name;
			}

			var selectElement = (IHTMLSelectElement) htmlElement;
			return selectElement.name;
		}

		private void Follow(Element elem)
		{
			if (elem is Form)
			{
				var form = (Form) elem;
				form.Submit();
			}

			if (elem is Link)
			{
				var link = (Link) elem;

				if (link.Url != null && !link.Url.Equals(ie.Url))
				{
					link.Click();
				}
			}
		}

		protected Checkable Has(string name)
		{
			return new Checkable(Locate(name) != null);
		}

		protected Element Locate(AttributeConstraint constraint)
		{
			return ie.Locate(constraint);
		}

		protected Element Locate(string name)
		{
			ExecutionSteps.Log.WriteLine("Locate {0}", name);
			return Locate(name, true);
		}

		protected Element Locate(string name, bool failFast)
		{
			return ie.Locate(name, failFast);
		}

		protected Element LocateLink(string name)
		{
			return ie.LocateLink(name);
		}

		protected Element[] LocateAll(string name)
		{
			return ie.LocateAll(name);
		}


		protected static Until Until(string s)
		{
			return new Until(s);
		}

		public static IDictionary<string, object> With(string key, object val)
		{
			var result =
				new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
			result.Add(key, val);
			return result;
		}

		public static IDictionary<string, object> With(object o)
		{
			var result =
				new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
			if (o == null)
				return result;
			if (o is IDictionary)
			{
				foreach (DictionaryEntry entry in (IDictionary) o)
					result.Add(entry.Key.ToString(), entry.Value);
			}
			else
			{
				foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(o))
					result.Add(prop.Name, prop.GetValue(o));
			}
			return result;
		}

		protected static void AssertThat(object check)
		{
		}

		protected static Checkable HasOption(SelectList selectList, string option)
		{
			Option opt = selectList.Option(option);
			return new Checkable(opt.Exists);
		}


		public static AttributeConstraint FindByHasAttr(string attrName)
		{
			return new AttributeConstraint(attrName, new HasAttrCompare());
		}


		public static AttributeConstraint FindByClassName(string className)
		{
			return new AttributeConstraint("classname", new NamesCompare(className));
		}


		[Obsolete("Use CSSClassMapper instead.")]
		protected static string PropertyToCSS<TDTO>(
			Expression<Func<TDTO, object>> memberExpr)
		{
			return ReflectionHelper.FindProperty(memberExpr).Name.ToLowerCamelCase();
		}

		#region Nested type: CSSValueHash

		protected class CSSValueHash : Dictionary<string, object>
		{
		}

		#endregion
	}
}