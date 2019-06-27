using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlogDemoApi.Controllers
{
    [Route("api/values")]
    public class ValueController:Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("hello");
        }
    }
}
