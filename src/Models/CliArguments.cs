using System;
using EntryPoint;

namespace naughty_strings_runner.Models
{
    [Help(
        "This program is intended to show off the key features of EntryPoint, such as this handy declarative API which includes a documentation generator")]
    public class CliArguments : BaseCliArguments
    {
        public CliArguments() : base("Naughty strings runner")
        {
        }

        [OptionParameter("domain")]
        [Help("Domain including protocal")]
        public string Domain { get; set; }

        [OptionParameter("query-param")]
        [Help("Name of the parameter that will be used for requests")]
        public string QueryParameter { get; set; }

        public void EnsureIsValid()
        {
            if (string.IsNullOrEmpty(Domain))
            {
                throw new Exception($"Please provide a valid domain: {Domain}");
            }

            if (string.IsNullOrEmpty(QueryParameter))
            {
                throw new Exception($"Please provide a valid query parameter: {QueryParameter}");
            }
        }
    }
}