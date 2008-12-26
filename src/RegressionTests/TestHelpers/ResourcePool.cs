using System;
using System.Collections;
using System.Collections.Generic;
using CodeCampServer.RegressionTests.TestHelpers.SmartWatiN;
using mshtml;
using RegressionTests;
using SHDocVw;
using WatiN.Core;

namespace CodeCampServer.RegressionTests.TestHelpers
{
	public class IeResourcePool
	{
		private readonly Stack availableResources;
		private readonly List<IntPtr> busyResources;
		private static  object Lock = new object();

		public IeResourcePool()
		{
			availableResources = Stack.Synchronized(new Stack());
			busyResources = new List<IntPtr>();
		}

		public SmartIE GetExistingInternetExplorerByHwnd(IntPtr hwnd)
		{
			var allBrowsers = new ShellWindows();

			foreach (InternetExplorer internetExplorer in allBrowsers)
			{
				try
				{
					if (internetExplorer.Document is IHTMLDocument2)
					{
						if (new IntPtr( internetExplorer.HWND) == hwnd)
						{
							return new SmartIE(internetExplorer);
						}
					}
				}
				catch
				{
				}
			}
			return null;
		}

		public static void ClearCache(SmartIE ie)
		{
			lock (Lock)
			{
				ie.ClearCache();
			}
		}
		public SmartIE Get()
		{
			SmartIE result = null;
			if (availableResources.IsEmpty())
			{
				result = CreateIE();
			}
			else
			{
				var hwnd = (IntPtr) availableResources.Pop();
				result = GetExistingInternetExplorerByHwnd(hwnd);
				if (result == null)
				{
					result = CreateIE();
				}
			}

			busyResources.Add(result.hWnd);

            
			result.AutoClose = false;
			ClearCache(result);            
			result.GoTo("about:blank");
			return result;
		}

		private SmartIE CreateIE()
		{
//        	Settings.Instance.SleepTime = 50; // defaults to 100 secs
			Settings.Instance.AttachToIETimeOut = 120; // defaults to 30 secs
			Settings.Instance.AutoStartDialogWatcher = false;
			Settings.Instance.AutoMoveMousePointerToTopLeft = false;
			var result = new SmartIE(true);
			result.ShowWindow(NativeMethods.WindowShowStyle.Maximize);            
			return result;
		}

		public void Release(SmartIE resource)
		{
			if (resource != null)
				if (busyResources.Remove(resource.hWnd))
					availableResources.Push(resource.hWnd);
		}
	}
}