using System;
using System.Collections.Generic;

namespace NDViet.UT.WS.AppConsole.Sessions
{
    public class UserInfo
    {        
        public string Name { get; set; }
        public string Email { get; set; }
        public class CardInfo
        {
            public Guid CardId { set; get; }
            public string AccNumber { set; get; }
            public string Status { set; get; }
            public string CardNumber { get; set; }
        }

        public List<CardInfo> Cards { set; get; }
    }
}