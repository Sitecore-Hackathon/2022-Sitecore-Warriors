using Microsoft.Extensions.Logging;
using Sitecore.DevEx.Client.Logging;
using Sitecore.DevEx.Configuration.Models;
using SitecoreWarriors.DevEx.Extensibility.Jobs.Commands;
using SitecoreWarriors.DevEx.Jobs.Client.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SitecoreWarriors.DevEx.Extensibility.Jobs.Tasks
{
    public class RebuildLinkDbTask
    {
        private readonly IRebuildLinkDbService _rebuildLinkDbService;
        private readonly ILogger<RebuildLinkDbTask> _logger;
        private readonly IEnvironmentConfigurationProvider _configurationProvider;

        public RebuildLinkDbTask(
          ILogger<RebuildLinkDbTask> logger,
          IEnvironmentConfigurationProvider configurationProvider,
          IRebuildLinkDbService startJobService)
        {
            this._rebuildLinkDbService = startJobService;
            this._logger = logger;
            this._configurationProvider = configurationProvider;
        }

        public async Task Execute(RebuildLinkDbArgs args)
        {
            EnvironmentConfiguration configurationAsync = await this._configurationProvider.GetEnvironmentConfigurationAsync(args.Config, args.EnvironmentName);
            Stopwatch stopwatch = Stopwatch.StartNew();
            string result = (await this._rebuildLinkDbService.RebuildLinkDbStartJobAsync(configurationAsync, args.Database).ConfigureAwait(false)).ToString();
            this._logger.LogTrace(string.Format("Rebuild Link DB Response: Loaded in {0}ms ({1}).", (object)stopwatch.ElapsedMilliseconds, args.Database));
            ColorLogExtensions.LogConsoleInformation((ILogger)this._logger, $"Rebuild Link DB:", new ConsoleColor?(ConsoleColor.Yellow), new ConsoleColor?());
            ColorLogExtensions.LogConsoleInformation((ILogger)this._logger, result, new ConsoleColor?(ConsoleColor.Green), new ConsoleColor?());
            stopwatch = (Stopwatch)null;
        }
    }
}
