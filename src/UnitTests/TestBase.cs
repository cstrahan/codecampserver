using Rhino.Mocks;

namespace CodeCampServer.UnitTests
{
	public abstract class TestBase
	{
		/// <summary>
		/// Create a mock
		/// </summary>
		/// <typeparam name="T">Type to be mocked</typeparam>
		/// <param name="argumentsForConstructor">Constructor arguments</param>
		/// <returns>T</returns>
		protected static T M<T>(params object[] argumentsForConstructor) where T : class
		{
			return MockRepository.GenerateMock<T>(argumentsForConstructor);
		}

		/// <summary>
		/// Create a stub
		/// </summary>
		/// <typeparam name="T">Type to be stubbed</typeparam>
		/// <param name="argumentsForConstructor">Constructor arguments</param>
		/// <returns>T</returns>
		protected static T S<T>(params object[] argumentsForConstructor) where T : class
		{
			return MockRepository.GenerateStub<T>(argumentsForConstructor);
		}
	}
}