using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace naughty_strings_runner.Services
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> Get(string url);
    }

    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "naughty-strings-runner");
            _httpClient.Timeout = TimeSpan.FromSeconds(10);
        }

        public async Task<HttpResponseMessage> Get(string url)
        {
            return await _httpClient.GetAsync(url);
        }
    }
}