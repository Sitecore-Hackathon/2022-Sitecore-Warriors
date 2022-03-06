using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using Sitecore.DevEx.Client.Tasks;
using SitecoreWarriors.DevEx.Extensibility.Jobs.Tasks;
using System;
using System.CommandLine;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace SitecoreWarriors.DevEx.Extensibility.Jobs.Commands
{
    [ExcludeFromCodeCoverage]
    public class StartJobCommand : SubcommandBase<StartJobTask, StartJobArgs>
    {
        public StartJobCommand(IServiceProvider container)
          : base("start", "Start a db task.", container)
        {
            ((Command)this).AddOption(ArgOptions.Config);
            ((Command)this).AddOption(ArgOptions.JobName);
            ((Command)this).AddOption(ArgOptions.Verbose);
            ((Command)this).AddOption(ArgOptions.Trace);
        }

        protected override async Task<int> Handle(StartJobTask task, StartJobArgs args)
        {
            ((TaskOptionsBase)args).Validate();
            await task.Execute(args).ConfigureAwait(false);
            return 0;
        }
    }
}
