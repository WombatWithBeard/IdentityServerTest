using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiOne.Controllers
{
    public class SecretController : Controller
    {
        [Route("/secret")]
        [HttpGet]
        [Authorize]
        public string Index()
        {
            return "Message from ApiOne";
        }
    }
}