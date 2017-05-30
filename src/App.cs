using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using naughty_strings_runner.Models;
using naughty_strings_runner.Services;

namespace naughty_strings_runner
{
    public class App
    {
        private readonly AppSettings _config;
        private readonly IHttpService _httpService;
        private readonly ILogger<App> _logger;
        private readonly INaughtyStringsProvider _naughtyStringsProvider;

        public App(
            INaughtyStringsProvider naughtyStringsProvider,
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
            var entries = Task.Run(() => _naughtyStringsProvider.GetStrings()).Result;

            var results = new List<HttpResponseMessage>();
            foreach (var entry in entries)
            {
                var result = Task.Run(() => _httpService.Get($"http://{_config.DefaultDomain}/produits?Search={entry}")).Result;
                results.Add(result);
            }

            foreach (var result in results.Where(x => !x.IsSuccessStatusCode))
            {
                _logger.LogError(result.RequestMessage.RequestUri.AbsolutePath);
            }

            Console.ReadKey();
        }
    }
}