using System;
using MbUnit.Framework;
using mshtml;
using RegressionTests.Web;
using WatiN.Core;

namespace RegressionTests.TestHelpers.SmartWatiN
{
	public class SmartIE : IE
	{
		public SmartIE(bool createInNewProcess) : base(createInNewProcess)
		{
		}

		public SmartIE(SHDocVw.InternetExplorer ie): base(ie)
		{
            
		}
		public override void WaitForComplete()
		{
			base.WaitForComplete();

			if (Element("naakErrors").Exists)
				Assert.Fail(string.Format(
				            	"The following NAAK ADA Validation Failures were found:\r\n{0}\r\n{1}",
				            	Element(Find.ById("naakErrors").ToString()),
				            	WebTest.CurrentTest().ErrorContext()));
		}
	}

	public class IEFactory
	{
        
		public static readonly IeResourcePool pool = new IeResourcePool();

		public static void Release(IE ie)
		{
			pool.Release((SmartIE) ie);
		}

		public static IE GetInternetExplorer()
		{            
			return pool.Get();
		}
	}
}