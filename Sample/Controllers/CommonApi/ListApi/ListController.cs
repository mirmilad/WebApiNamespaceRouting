using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Sample.Controllers.CommonApi.ListApi
{
    public class ListController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok("List");
        }
    }
}