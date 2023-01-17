using System;
using System.Collections.Generic;
using System.Text;

namespace NDViet.UT.WS.AppConsole.Sessions
{
    public interface ISessionService
    {
        UserInfo GetCurrentUser();
        bool IsLoggedIn();
    }
}
