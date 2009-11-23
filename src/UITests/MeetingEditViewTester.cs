using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Linq.Expressions;
using CodeCampServer.Core.Common;
using CodeCampServer.UI.Models.Input;
using NUnit.Framework;
using UITestHelper;
using WatiN.Core;

namespace CodeCampServerUiTests
{
	[TestFixture]
	public class MeetingEditViewTester
	{
		private IE _ie;

		[SetUp]
		public void setup()
		{
			System.AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
			var baseurl = System.Configuration.ConfigurationManager.AppSettings["url"];
			_ie = new IE(baseurl + "/Meeting/New");
		}

		void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			throw new NotImplementedException();
		}
		[TearDown]
		public void teardown()
		{
			_ie.Dispose();
		}
		public FluentForm<T> Form<T>()
		{
			return new FluentForm<T>(new WatinDriver(_ie));
		}

		[Test]
		public void Should_create_a_new_meeting()
		{
			Form<MeetingInput>()
				.WithText(m => m.Name, "TX")
				.WithText(m => m.Topic, "my topic")
				.WithHtml(m => m.Summary, "this will be a normal meeting")
				.WithHtml(m => m.Description, "The description")
				.WithText(m => m.Key, "foe")
				.WithText(m => m.LocationName, "our location")
				.WithText(m => m.LocationUrl, "http://foolocation.com")
				.WithHtml(m => m.SpeakerBio, "this is a great speaker")
				.WithText(m => m.SpeakerName, "bart simpson")
				.WithText(m => m.SpeakerUrl, "http://thesimpsons.com")
				.WithText(m => m.TimeZone, "CST")
				.WithText(m => m.EndDate, "12/12/2009")
				.WithText(m => m.StartDate, "12/12/2009")
				.Submit("submitForm");


			//Assert New page

		}
		
	}

	public static class FluentFormExtensions
	{
		public static FluentForm<TFormType> WithHtml<TFormType>(this FluentForm<TFormType> form,Expression<Func<TFormType, object>> property, string text)
		{
			form.InputWrappers.AddLast(new HtmlWrapper(property, text));
			return form;
		}
	}

	public class WatinDriver : IBrowserDriver
	{
		private readonly IE _ie;

		public WatinDriver(IE ie)
		{
			_ie = ie;
			_ie.ShowWindow(NativeMethods.WindowShowStyle.Maximize);
		}

		public void SetInput(IInputWrapper wrapper)
		{
			try
			{
				if (wrapper is HtmlWrapper)
				{
					var input = wrapper as InputWrapperBase;
					var name = UINameHelper.BuildNameFrom(input.Property);
					_ie.RunScript(("tinyMCE.execInstanceCommand('" + name + "', 'mceSetContent', false, '" + input.Value + "')"));
				}
				else if (wrapper is InputWrapperBase)
				{
					var input = wrapper as InputWrapperBase;
					var name = UINameHelper.BuildNameFrom(input.Property);
					TextField textField = _ie.TextField(Find.ByName(name));
					textField.Value = input.Value;
				}
			}
			catch (Exception)
			{
				CaptureScreenShot(GetTestname());
				throw;
			}
		}

		private void CaptureScreenShot(string testname) {
			Bitmap desktopBMP = new Bitmap(
				System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width,
				System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);

			Graphics g = Graphics.FromImage(desktopBMP);

			g.CopyFromScreen(0, 0, 0, 0,
			                 new Size(
			                 	System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width,
			                 	System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height));
			desktopBMP.Save(@".\"+testname+".jpg",ImageFormat.Jpeg);
			g.Dispose();
		}

		private string GetTestname() {
			var stack = new StackTrace();
			var testMethodFrame = stack.GetFrames().Reverse().Where(frame => frame.GetMethod().ReflectedType.Assembly == GetType().Assembly).
				FirstOrDefault();
			return testMethodFrame.GetMethod().Name;
		}

		public void ClickButton(string name)
		{
			_ie.Button(Find.By("rel",name)).Click();
		}
	}

	public class HtmlWrapper:InputWrapperBase
	{
		public HtmlWrapper(Expression expression, object value) : base(expression, value)
		{
			
		}
	}
}