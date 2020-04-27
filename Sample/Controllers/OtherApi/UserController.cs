using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Sample.Controllers.OtherApi
{
    public class UserController : ApiController
    {
        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        [HttpPost]
        public IHttpActionResult Login(LoginRequest request)
        {
            return Ok("Other");
        }
    }
}