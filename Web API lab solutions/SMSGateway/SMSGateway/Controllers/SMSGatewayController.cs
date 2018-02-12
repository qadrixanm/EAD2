using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using SMSGateway.Models;

namespace SMSGateway.Controllers
{
    public class SMSGatewayController : ApiController
    {
        private const int MaxSize = 140;
        private const String LOGFILENAME = "C:\\Gary\\data\\log.txt";

        // POST /api/SMSGateway/
        public IHttpActionResult PostSendSMS(TextMessage message)                         // message serialised in request body
        {
            if (ModelState.IsValid)
            {
                // log to file
                log("Sent: " + message.Content + " from " + message.FromNumber + " to " + message.ToNumber);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // log to file, may need to run VS as adminstrator in order to have write access to the file system
        [NonAction]                                                             // not a controller action
        private void log(String logInfo)
        {
            using(StreamWriter stream = File.AppendText(LOGFILENAME))
            {
                stream.Write(logInfo);
                stream.WriteLine(" " + DateTime.Now);
                stream.Close();
            }
        }
    }
}
