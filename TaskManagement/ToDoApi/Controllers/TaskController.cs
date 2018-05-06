using System.Collections.Generic;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("api")]
    public class TaskController : System.Web.Http.ApiController
    {
        [HttpGet]
        [Route("public")]
        public IHttpActionResult Public()
        {
            return Ok("This is a public endpoint. Don't need to be authenticated");
        }

        [HttpGet]
        [Route("tasks")]
        [Authorize]
        public IHttpActionResult GetTasks()
        {
            List<string> data = new List<string>
            {
                "Data line 1",
                "data line 2",
                "Hello from a private endpoint. You need to be authenticated."

            };
            return Ok(data);
        }
    }
}