using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace api_gateway.Api.Controllers
{
    public class CandidateProxyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public CandidateProxyController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet("Candidate/{candidateId}")]
        public async Task<IActionResult> GetCandidate([FromRoute] string candidateId)
        {
            
            var baseAddress = _configuration["ReverseProxy:Clusters:candidateCluster:Destinations:destination1:Address"];
            if (string.IsNullOrEmpty(baseAddress))
                return BadRequest("Candidate service address not configured.");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(baseAddress);

            //if (Request.Headers.TryGetValue("Authorization", out var authHeaderValues))
            //{
            //    var authHeader = authHeaderValues.ToString();
            //    if (!string.IsNullOrWhiteSpace(authHeader))
            //    {
            //        try
            //        {
            //            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(authHeader);
            //        }
            //        catch (FormatException)
            //        {
            //            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authHeader);
            //        }
            //    }
            //}

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJoZXlAZW1haWwuY29tIiwianRpIjoiNmEzMjg5NTQtZTQ1OC00Nzk3LWFhN2YtNTZmYzA5MTg5NjdmIiwiaWF0IjoxNzc2OTYxMzY2LCJuYmYiOjE3NzY5NjEzNjYsImV4cCI6MTc3Njk2NDk2NiwiaXNzIjoiSWRlbnRpdHlTZXJ2aWNlIiwiYXVkIjoiSWRlbnRpdHlTZXJ2aWNlQ2xpZW50cyJ9.D57X-k4RzzNLCOBxMsdkzIwYwrgiUw2I2_rQx6Sr76c");

            var response = await client.GetAsync($"Candidate/candidate/{candidateId}");
            var content = await response.Content.ReadAsStringAsync();

            return StatusCode((int)response.StatusCode, content);
        }
    }
}
