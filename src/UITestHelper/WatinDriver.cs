using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using CodeCampServer.Core.Common;
using MvcContrib.UI.InputBuilder;
using WatiN.Core;

namespace UITestHelper
{
	public class WatinDriver : IBrowserDriver
	{
		public WatinDriver(IE ie)
		{
			IE = ie;
			IE.ShowWindow(NativeMethods.WindowShowStyle.Maximize);
		}

		public IE IE { get; set; }


		public void SetInput<TFormType>(InputWrapperBase<TFormType> wrapper)
		{
			var propertyInfo = ReflectionHelper.FindProperty(wrapper.Property);
			var name = UINameHelper.BuildNameFrom(wrapper.Property);

			var conventions = InputBuilder.Conventions.ToArray();
			

			Type factoryType;

			foreach (var factory in conventions)
			{
				if(factory.CanHandle(propertyInfo))
				{
					factoryType = factory.GetType();
					break;
				}

				throw new InvalidOperationException("Could not find an Input Builder convention(IPropertyViewModelFactory) for type:" + propertyInfo.PropertyType + " and Name:" + name);
			}
		}

		public virtual void CaptureScreenShot(string testname) {
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

		public virtual string GetTestname() {
			var stack = new StackTrace();
			var testMethodFrame = stack.GetFrames().Reverse().Where(frame => frame.GetMethod().ReflectedType.Assembly == GetType().Assembly).
				FirstOrDefault();
			return testMethodFrame.GetMethod().Name;
		}

		public virtual void ClickButton(string name)
		{
			IE.Button(Find.By("rel",name)).Click();
		}
	}
}