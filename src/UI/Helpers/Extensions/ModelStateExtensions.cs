using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CodeCampServer.UI.Helpers
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

		public static void AddModelErrors(this ModelStateDictionary state, IDictionary<string, string> bag)
		{
			foreach (string key in bag.Keys)
			{
				state.AddModelError(key, bag[key]);
			}
		}

		public static void AddModelErrors(this ModelStateDictionary state, IDictionary<string, string[]> bag)
		{
			foreach (string key in bag.Keys)
			{
				foreach (string value in bag[key])
				{
					state.AddModelError(key, value);
				}
			}
		}
        //public static void AddException(this ModelStateDictionary state,System.Exception e)
        //{
        //    while(e.InnerException!=null)
        //    {
        //        e = e.InnerException;
        //    }
        //    state.AddModelError()
        //        .Add("error",e.Message);
        //}
	}
}