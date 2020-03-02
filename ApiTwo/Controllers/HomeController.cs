using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace ApiTwo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("/")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var serverClient = _httpClientFactory.CreateClient();

            var discoveryDocument = await serverClient.GetDiscoveryDocumentAsync("https://localhost:44383/");

            var tokenResponse =
                await serverClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = discoveryDocument.TokenEndpoint,
                    ClientId = "client_id",
                    ClientSecret = "client_secret",
                    Scope = "ApiOne"
                });

            //Get data from ApiOne
            var apiClient = _httpClientFactory.CreateClient();

            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync("http://localhost:48078/secret");

            var content = await response.Content.ReadAsStringAsync();
            
            return Ok(new
            {
                access_token = tokenResponse.AccessToken,
                message = content
            });
        }
    }
}