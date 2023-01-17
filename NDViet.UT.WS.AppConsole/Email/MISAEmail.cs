using System;

namespace NDViet.UT.WS.AppConsole.Email
{
    public class MISAEmail
    {
        public void Send(EmailDto email)
        {
            Console.WriteLine($"[Real] Send email success:=======>\n- From {email.From}\n- To {email.To}\n- Content: {email.Content}\n<=======");
        }
    }
}