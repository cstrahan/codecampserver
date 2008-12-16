using System;
using Gallio.Framework.Pattern;
using MbUnit.Framework;

namespace RegressionTests
{
	[AssemblyFixture]
	public class AssemblyFixture
	{
		private readonly string DEFAULT_DEGREE_OF_PARALLELISM = Environment.ProcessorCount.ToString();

		public AssemblyFixture()
		{
			string DegreeOfParallelism = Environment.GetEnvironmentVariable("DEGREE_OF_PARALLELISM") ??
			                             DEFAULT_DEGREE_OF_PARALLELISM;
			PatternTestGlobals.DegreeOfParallelism = Convert.ToInt32(DegreeOfParallelism);
		}
	}
}