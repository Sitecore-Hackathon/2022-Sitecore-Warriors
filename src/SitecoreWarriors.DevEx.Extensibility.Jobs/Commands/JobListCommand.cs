using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using Sitecore.DevEx.Client.Tasks;
using SitecoreWarriors.DevEx.Extensibility.Jobs.Tasks;
using System;
using System.CommandLine;
using System.Threading.Tasks;

namespace SitecoreWarriors.DevEx.Extensibility.Jobs.Commands
{
    public class JobListCommand : SubcommandBase<JobListTask, JobTaskOptions>
    {
        public JobListCommand(IServiceProvider container)
          : base("list", "Get all jobs list (running, queued, finished and db task jobs). Db task can be started on-demand.", container)
        {
            ((Command)this).AddOption(ArgOptions.Config);
            ((Command)this).AddOption(ArgOptions.Verbose);
            ((Command)this).AddOption(ArgOptions.Trace);
        }

        protected override async Task<int> Handle(JobListTask task, JobTaskOptions args)
        {
            ((TaskOptionsBase)args).Validate();
            await task.Execute(args).ConfigureAwait(false);
            return 0;
        }
    }
}
