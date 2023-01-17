using System;
using System.Collections.Generic;
using System.Text;

namespace NDViet.UT.WS.AppConsole.Utilities
{
    public class CommonFunction
    {
       public  static string DateTimeFormat(DateTime dateValue)
        {
            return dateValue.ToString("dd/MM/yyyy hh:mm:ffff");
        }
    }
}
