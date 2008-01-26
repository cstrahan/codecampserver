using System;
using System.Collections.Generic;
using System.Text;
using StructureMap;

namespace CodeCampServer.Model.Security
{
    [PluginFamily(Keys.DEFAULT)]
    public interface IAuthorizationService
    {
        bool IsAdministrator { get; }
    }
}
