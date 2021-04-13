using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController : Controller
    {
        public string Index()
        {
            return "Healthy";
        }

        [HttpGet]
        [Route("gettest")]
        public string GetTest()
        {
            return "I returned!";
        }
    }
}