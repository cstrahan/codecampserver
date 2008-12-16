using System.Linq;
using System.Web.Mvc;

namespace CodeCampServer.UI
{
	public static class ModelStateExtensions
	{
		public static bool IsInvalid(this ModelStateDictionary state)
		{
			return !state.IsValid;
		}

		public static bool IsInvalid(this ModelStateDictionary state, string prefix)
		{
			if (state.IsValid) return false;
			return state.Count(kvp => kvp.Key.StartsWith(prefix) && kvp.Value.Errors.Count > 0) > 0;
		}
	}
}