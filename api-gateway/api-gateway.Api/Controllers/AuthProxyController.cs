using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using api_gateway.Api.Models.IdentityService;

namespace api_gateway.Api.Controllers
{
    [ApiController]
    [Route("proxy")]
    public class AuthProxyController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AuthProxyController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // POST /proxy/login
        // Forwards the provided LoginRequest to the configured upstream auth service's /Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null)
                return BadRequest("Request body is required.");

            // Read upstream address from configuration (appsettings.json)
            var baseAddress = _configuration["ReverseProxy:Clusters:authCluster:Destinations:destination1:Address"];
            if (string.IsNullOrEmpty(baseAddress))
                return BadRequest("Upstream auth service address not configured.");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(baseAddress);

            // Post to upstream /Auth/login
            var response = await client.PostAsJsonAsync("Auth/login", request);
            var content = await response.Content.ReadAsStringAsync();

            return StatusCode((int)response.StatusCode, content);
        }

        // POST /proxy/logout
        // Forwards the provided LogoutRequest to the configured upstream auth service's /Auth/logout
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
        {
            if (request == null)
                return BadRequest("Request body is required.");

            var baseAddress = _configuration["ReverseProxy:Clusters:authCluster:Destinations:destination1:Address"];
            if (string.IsNullOrEmpty(baseAddress))
                return BadRequest("Upstream auth service address not configured.");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(baseAddress);

            // Post to upstream /Auth/logout
            var response = await client.PostAsJsonAsync("Auth/logout", request);
            var content = await response.Content.ReadAsStringAsync();

            return StatusCode((int)response.StatusCode, content);
        }

        // POST /proxy/register
        // Forwards the provided RegisterRequest to the configured upstream auth service's /Auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (request == null)
                return BadRequest("Request body is required.");

            var baseAddress = _configuration["ReverseProxy:Clusters:authCluster:Destinations:destination1:Address"];
            if (string.IsNullOrEmpty(baseAddress))
                return BadRequest("Upstream auth service address not configured.");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(baseAddress);

            // Post to upstream /Auth/register
            var response = await client.PostAsJsonAsync("Auth/register", request);
            var content = await response.Content.ReadAsStringAsync();

            return StatusCode((int)response.StatusCode, content);
        }

        // POST /proxy/refresh
        // Forwards the provided RefreshRequest to the configured upstream auth service's /Auth/refresh
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            if (request == null)
                return BadRequest("Request body is required.");

            var baseAddress = _configuration["ReverseProxy:Clusters:authCluster:Destinations:destination1:Address"];
            if (string.IsNullOrEmpty(baseAddress))
                return BadRequest("Upstream auth service address not configured.");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(baseAddress);

            // Post to upstream /Auth/refresh
            var response = await client.PostAsJsonAsync("Auth/refresh", request);
            var content = await response.Content.ReadAsStringAsync();

            return StatusCode((int)response.StatusCode, content);
        }
    }
}
