using System;
using System.Web.Mvc;

namespace CodeCampServer.UI
{
    public interface ISubController : IController
    {
        Action GetResult(ControllerBase parentController);
    }
}