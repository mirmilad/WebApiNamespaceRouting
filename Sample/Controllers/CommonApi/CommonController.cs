using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Sample.Controllers.CommonApi
{
    public class CommonController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok("Common");
        }
    }
}