using System.Net.Http;
using System.Web.Http;

namespace securewebapi
{
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("api")]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage
            {
                Content = new StringContent("Hello HTTP")
            };
        }
    }
}
