using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using naughty_strings_runner.Models;
using naughty_strings_runner.Services;

namespace naughty_strings_runner
{

    public class App
    {
        private readonly INaughtyStringsProvider _naughtyStringsProvider;
        private readonly ILogger<App> _logger;
        private readonly IHttpService _httpService;
        private readonly AppSettings _config;

        public App(INaughtyStringsProvider naughtyStringsProvider,
            IOptions<AppSettings> config,
            ILogger<App> logger,
            IHttpService httpService)
        {
            _naughtyStringsProvider = naughtyStringsProvider;
            _logger = logger;
            _httpService = httpService;
            _config = config.Value;
        }

        public void Run()
        {
            _logger.LogInformation($"This is a console application for {_config.Title}");

            var entries = Task.Run(() => _naughtyStringsProvider.GetStrings()).Result;
            foreach (var entry in entries.Take(1))
            {
                var reponse = Task.Run(() => _httpService.Get($"http://{_config.DefaultDomain}?r={entry}")).Result;
                _logger.LogInformation($"{reponse.StatusCode}");
            }

            System.Console.ReadKey();
        }
    }
}
