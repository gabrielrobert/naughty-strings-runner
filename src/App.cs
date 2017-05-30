using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EntryPoint;
using Microsoft.Extensions.Logging;
using naughty_strings_runner.Models;
using naughty_strings_runner.Services;

namespace naughty_strings_runner
{
    public class App : BaseCliCommands
    {
        private readonly IHttpService _httpService;
        private readonly ILogger<App> _logger;
        private readonly INaughtyStringsProvider _naughtyStringsProvider;

        public App(
            INaughtyStringsProvider naughtyStringsProvider,
            ILogger<App> logger,
            IHttpService httpService)
        {
            _naughtyStringsProvider = naughtyStringsProvider;
            _logger = logger;
            _httpService = httpService;
        }

        [DefaultCommand]
        [Command("run")]
        [Help("Send all naughty strings to an endpoint using HTTP requests")]
        public void Run(string[] args)
        {
            var arguments = GetArguments(args);
            var entries = Task.Run(() => _naughtyStringsProvider.GetStrings()).Result;
            var results = SendRequests(entries, arguments);
            ShowErrors(results);
            Console.ReadKey();
        }

        private void ShowErrors(List<HttpResponseMessage> results)
        {
            foreach (var result in results.Where(x => !x.IsSuccessStatusCode))
            {
                _logger.LogError(result.RequestMessage.RequestUri.AbsolutePath);
            }
        }

        private List<HttpResponseMessage> SendRequests(List<string> entries, CliArguments arguments)
        {
            var results = new List<HttpResponseMessage>();
            foreach (var entry in entries)
            {
                var result = Task.Run(() => _httpService.Get($"{arguments.Domain}?{arguments.QueryParameter}={entry}"));
                results.Add(result.Result);
            }
            return results;
        }

        private CliArguments GetArguments(string[] args)
        {
            var arguments = Cli.Parse<CliArguments>(args);
            arguments.EnsureIsValid();
            return arguments;
        }
    }
}