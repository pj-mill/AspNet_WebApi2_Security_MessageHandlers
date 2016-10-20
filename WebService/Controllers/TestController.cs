using System.Web.Http;

namespace WebService.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {

        [Authorize]
        [HttpGet]
        [Route("testauthpoint")]
        public IHttpActionResult TestAuthPoint()
        {
            return Ok("Ok From Authorized Controller Action");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("testpoint")]
        public IHttpActionResult TestPoint()
        {
            return Ok("Ok From Anonymous Controller Action");
        }
    }
}