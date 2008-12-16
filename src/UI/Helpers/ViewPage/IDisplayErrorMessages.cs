using System.Web.Mvc;

namespace CodeCampServer.UI.ViewPage
{
    public interface IDisplayErrorMessages
    {
        string Display();
        ModelStateDictionary ModelState { set; }
    }
}