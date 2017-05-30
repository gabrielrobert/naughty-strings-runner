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
    }
}