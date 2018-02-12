// an API controller which acts as a RESTful web service
// GET a hello message for a specified name

using System.Web.Http;

namespace HelloWorld.Controllers
{
    // RESTful service
    public class HelloController : ApiController                // is a API Controller
    {
        // GET /api/Hello/Gary or /api/Hello?name=Gary
        public IHttpActionResult GetHelloGreeting(string name)               // GET
        {
            return Ok("Hello there " + name + ", welcome to the ASP.Net Web API");      // 200 OK
        }

        // return data serialised as XML or JSON or GSON depending on Accept header
        // sample URLs to invoke
        // http://localhost:1107/api/Hello/Gary or http://localhost:1107/api/Hello?name=Gary

        // stateless, new controller instance constructed to handle each request
    }
}

