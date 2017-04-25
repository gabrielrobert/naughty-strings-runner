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
        private readonly AppSettings _config;

        public App(INaughtyStringsProvider naughtyStringsProvider,
            IOptions<AppSettings> config,
            ILogger<App> logger)
        {
            _naughtyStringsProvider = naughtyStringsProvider;
            _logger = logger;
            _config = config.Value;
        }

        public void Run()
        {
            _logger.LogInformation($"This is a console application for {_config.Title}");
            _naughtyStringsProvider.GetStrings();
            System.Console.ReadKey();
        }
    }
}
