using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NDViet.UT.WS.AppConsole.Exceptions
{
    public class BussinessException : Exception
    {
        public string ErrorCode { set; get; }
        public List<ErrorDetail> Errors { set; get; } = new List<ErrorDetail>();
        public BussinessException(List<ErrorDetail> errors)
        {
            Errors = errors;
        }

        public BussinessException(string errorCode, List<string> messages)
        {
            
            foreach (var msg in messages)
            {
                Errors.Add(new ErrorDetail() { ErrorCode = errorCode, Message = msg });
            }
        }

        public BussinessException(string errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
        public class ErrorDetail
        {
            public string ErrorCode { set; get; }
            public string Message { set; get; } 
        }

    }
}
