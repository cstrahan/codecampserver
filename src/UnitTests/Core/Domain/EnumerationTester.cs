using System;
using System.Collections.Generic;
using System.Reflection;
using CodeCampServer.Core.Domain.Model.Enumerations;
using NUnit.Framework;

namespace CodeCampServer.UnitTests.Core.Domain
{
	[TestFixture]
	public class EnumerationTester
	{
		[Test]
		public void Test_all_enumerations()
		{
			Assembly assembly = Assembly.LoadFrom("CodeCampServer.Core.dll");

			foreach (var type in assembly.GetTypes())
			{
				if (typeof (Enumeration).IsAssignableFrom(type))
				{
					var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

					var values = new List<int>();
					var displayNames = new List<string>();

					foreach (var info in fields)
					{
						object instance = Activator.CreateInstance(type);

						var locatedValue = (Enumeration) info.GetValue(instance);

						if (locatedValue != null)
						{
							if (!values.Contains(locatedValue.Value))
							{
								values.Add(locatedValue.Value);
							}
							else
							{
								Assert.Fail(string.Format("Enumeration {0} has duplicate value {1}", type.Name, locatedValue.Value));
							}

							if (!displayNames.Contains(locatedValue.DisplayName))
							{
								displayNames.Add(locatedValue.DisplayName);
							}
							else
							{
								Assert.Fail(string.Format("Enumeration {0} has duplicate display name {1}", type.Name, locatedValue.DisplayName));
							}
						}
					}
				}
			}
		}
	}
}