using System.Net.Http;

namespace naughty_strings_runner.Services
{
    public interface IHttpService
    {
    }

    public class HttpService : IHttpService
    {
        private HttpClient GetHttpClient()
        {
            return new HttpClient();
        }
    }
}