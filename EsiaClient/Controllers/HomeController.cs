using System.Net.Http;
using EsiaClient.Esia;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EsiaClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var client = new HttpClient();
            var uri = new EsiaModel().GetEsiaUri();
            var response = client.GetAsync(uri);

            var result = response.Result;

            return View("Index", uri);
        }

        [HttpGet]
        public IActionResult GetEsiaResponse()
        {
            return View();
        }

        [HttpPost]
        public void GetToken(string model)
        {
            var test = JsonConvert.DeserializeObject(model);
        }
        
    }
}