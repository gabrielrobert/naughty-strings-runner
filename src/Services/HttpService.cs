using System.Net.Http;
using System.Threading.Tasks;

namespace naughty_strings_runner.Services
{
    public interface IHttpService
    {
    }

    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "naughty-strings-runner");
        }

        public async Task<HttpResponseMessage> Get(string url)
        {
            return await _httpClient.GetAsync(url);
        }
    }
}