using System;
using System.Collections.Generic;
using System.Text;

namespace NDViet.UT.WS.AppConsole.Sessions
{
    public class SessionService : ISessionService
    {
        public bool IsLoggedIn()
        {
            Console.WriteLine("[Real] User logged in!");
            return true;
        }
        public UserInfo GetCurrentUser()
        {
            Console.WriteLine("[Real] Get current user!");
            return new UserInfo()
            {
                Email = "nguyenvana@gmail.com",
                Cards = new List<UserInfo.CardInfo>()
                {
                    new UserInfo.CardInfo()
                    {
                        CardId = new Guid("26d2ade9-95c4-42d4-adae-7ffb803ce8e6"),
                        AccNumber="011204560709",
                    }
                },
                Name="Nguyen Van A"
            };
        }
    }
}
