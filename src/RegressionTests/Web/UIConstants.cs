using System;

namespace RegressionTests.Web
{
	public static class UIConstants
	{
		private const string DEFAULT_URL = "http://localhost:8082";
		private static readonly string DEFAULT_RECORD_FIELD_ACCESS_FLAG = false.ToString();

		public static string BASE_URL
		{
			get
			{
				string jcmsSite = Environment.GetEnvironmentVariable("JCMS_SITE") ?? DEFAULT_URL;
				return jcmsSite;
			}
		}

		// To report data field coverage audit
		public static bool RECORD_FIELD_ACCESS
		{
			get
			{
				string result = Environment.GetEnvironmentVariable("RECORD_FIELD_ACCESS") ?? DEFAULT_RECORD_FIELD_ACCESS_FLAG;
				return bool.Parse(result.Trim());
			}
		}
	}
}