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
    public class RebuildLinkDbCommand : SubcommandBase<RebuildLinkDbTask, RebuildLinkDbArgs>
    {
        public RebuildLinkDbCommand(IServiceProvider container)
          : base("rebuildlinkdb", "Start rebuilding a link db.", container)
        {
            ((Command)this).AddOption(ArgOptions.Config);
            ((Command)this).AddOption(ArgOptions.Database);
            ((Command)this).AddOption(ArgOptions.Verbose);
            ((Command)this).AddOption(ArgOptions.Trace);
        }

        protected override async Task<int> Handle(RebuildLinkDbTask task, RebuildLinkDbArgs args)
        {
            ((TaskOptionsBase)args).Validate();
            await task.Execute(args).ConfigureAwait(false);
            return 0;
        }
    }
}
