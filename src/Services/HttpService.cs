using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace naughty_strings_runner.Services
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> Get(string url);
    }

    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<App> _logger;

        public HttpService(
            ILogger<App> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "naughty-strings-runner");
            _httpClient.Timeout = TimeSpan.FromSeconds(20);
        }

        public async Task<HttpResponseMessage> Get(string url)
        {
            _logger.LogInformation(url);

            try
            {
                var response = await _httpClient.GetAsync(url);
                return response;
            }
            catch (TaskCanceledException e)
            {
                _logger.LogError($"{url} failed.");
                return null;
            }
        }
    }
}