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
            _logger.LogInformation($"This is a console application for {_config.Title}");

            var entries = Task.Run(() => _naughtyStringsProvider.GetStrings()).Result;
            var queries = entries.Select(entry => $"http://{_config.DefaultDomain}/produits?Search={entry}");

            var results = new List<HttpResponseMessage>();
            foreach (var query in queries)
            {
                var result = Task.Run(() => _httpService.Get(query)).Result;
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