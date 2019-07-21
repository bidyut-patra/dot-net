using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Authentication.Service.Controllers
{
    [RoutePrefix("api")]
    public class AuthenticationController : ApiController
    {
        public IAuthenticationService AuthService { get; set; }
        public AuthenticationController(IAuthenticationService authService)
        {
            this.AuthService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IHttpActionResult> Login(UserInfo userInfo)
        {
            var authInfo = await Task.Run(() =>
            {
                return this.AuthService.Login(userInfo);
            });
            return Ok(authInfo);
        }

        [HttpPost]
        [Route("logoff")]
        public async Task<IHttpActionResult> Logoff(string token)
        {
            await Task.Run(() =>
            {
                var authTokenService = this.AuthService as IAuthenticationTokenService;
                authTokenService.Logoff(token);
            });
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("authtypes")]
        public async Task<IHttpActionResult> GetAuthTypes()
        {
            await Task.Run(() => { });
            return Ok(new List<string>() { "A", "B" });
        }

        [HttpGet]
        [Route("session")]
        public async Task<IHttpActionResult> GetSession(string token)
        {
            var session = await Task.Run(() => 
            {
                return this.AuthService.GetSession(token);
            });
            return Ok(session);
        }
    }
}
