using System.Linq;
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
            var claims = User.Claims.ToList();
            
            return "Message from ApiOne";
        }
    }
}