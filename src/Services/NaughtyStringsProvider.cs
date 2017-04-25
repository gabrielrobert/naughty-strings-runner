using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using naughty_strings_runner.Models;
using Newtonsoft.Json;
using Octokit;

namespace naughty_strings_runner.Services
{
    public interface INaughtyStringsProvider
    {
        Task<List<string>> GetStrings();
    }

    internal class NaughtyStringsProvider : INaughtyStringsProvider
    {
        private readonly AppSettings _config;
        private readonly ILogger<NaughtyStringsProvider> _logger;

        public NaughtyStringsProvider(
            ILogger<NaughtyStringsProvider> logger,
            IOptions<AppSettings> config)
        {
            _logger = logger;
            _config = config.Value;
        }

        public async Task<List<string>> GetStrings()
        {
            var fileContent = await GetContent();
            var naughtyStrings = JsonConvert.DeserializeObject<List<string>>(fileContent);

            if (naughtyStrings.Any())
            {
                _logger.LogInformation($"Naughty strings loaded.");
            }

            return naughtyStrings;
        }

        private async Task<string> GetContent()
        {
            var github = new GitHubClient(new ProductHeaderValue(_config.Title));
            _logger.LogInformation("Accessing naughty strings using Github API...");
            var files = await github.Repository.Content.GetAllContents(
                "minimaxir",
                "big-list-of-naughty-strings",
                "blns.json"
            );
            return files.FirstOrDefault().Content;
        }
    }
}