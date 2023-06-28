using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioTerra.Application.Dto.Response
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public string ErrorDetails { get; set; }

        public ErrorResponse(string message)
        {
            Message = message;
        }

        public ErrorResponse(string message, Exception error)
        {
            Message = message;
            ErrorDetails = error.Message;
        }

    }

}
