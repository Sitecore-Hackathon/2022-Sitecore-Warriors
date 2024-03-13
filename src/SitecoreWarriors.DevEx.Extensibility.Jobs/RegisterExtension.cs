using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sitecore.Devex.Client.Cli.Extensibility;
using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using Sitecore.DevEx.Core.Client;
using SitecoreWarriors.DevEx.Extensibility.Jobs.Commands;
using SitecoreWarriors.DevEx.Extensibility.Jobs.Tasks;
using SitecoreWarriors.DevEx.Jobs.Client;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Diagnostics.CodeAnalysis;

namespace SitecoreWarriors.DevEx.Extensibility.Jobs
{
    public class RegisterExtension : ISitecoreCliExtension
    {
        public IEnumerable<ISubcommand> AddCommands(IServiceProvider container) => (IEnumerable<ISubcommand>)new ISubcommand[1]
        {
            RegisterExtension.CreateJobCommand(container)
        };

        [ExcludeFromCodeCoverage]
        public void AddConfiguration(IConfigurationBuilder configBuilder)
        {
        }

        public void AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSitecoreApiClient()
                .AddJobServices()
                .AddSingleton<JobListCommand>()
                .AddSingleton<RebuildLinkDbCommand>()
                .AddSingleton<StartJobCommand>();
            serviceCollection.TryAddTransient<IEnvironmentConfigurationProvider, EnvironmentConfigurationProvider>();
        }

        private static ISubcommand CreateJobCommand(IServiceProvider container)
        {
            JobsCommand jobCommand = new JobsCommand("job", "working with Sitecore job");
            jobCommand.AddAlias("job");
            jobCommand.AddCommand((Command)container.GetRequiredService<JobListCommand>());
            jobCommand.AddCommand((Command)container.GetRequiredService<RebuildLinkDbCommand>());
            jobCommand.AddCommand((Command)container.GetRequiredService<StartJobCommand>());
            return (ISubcommand)jobCommand;
        }
    }
}
