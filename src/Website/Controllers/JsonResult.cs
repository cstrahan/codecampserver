using System.Web.Mvc;

namespace CodeCampServer.Website.Controllers
{
    public class JsonResult : ActionResult
    {
        private readonly string _json;

        public JsonResult(string json)
        {
            _json = json;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "text/json";
            context.HttpContext.Response.Write(_json);
        }
    }
}