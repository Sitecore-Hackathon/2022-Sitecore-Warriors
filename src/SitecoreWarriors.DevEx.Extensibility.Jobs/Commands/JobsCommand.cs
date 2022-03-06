using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using System.CommandLine;

namespace SitecoreWarriors.DevEx.Extensibility.Jobs.Commands
{
  public class JobsCommand : Command, ISubcommand
  {
    public JobsCommand(string name, string description = null)
      : base(name, description)
    {
    }
  }
}
