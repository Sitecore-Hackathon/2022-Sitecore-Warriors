using Microsoft.Extensions.Logging;
using Sitecore.DevEx.Client.Logging;
using Sitecore.DevEx.Configuration.Models;
using SitecoreWarriors.DevEx.Jobs.Client.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SitecoreWarriors.DevEx.Extensibility.Jobs.Tasks
{
    public class JobListTask
    {
        private readonly ILogger<JobListTask> _logger;
        private readonly IJobListService _jobListService;
        private readonly IEnvironmentConfigurationProvider _configurationProvider;

        public JobListTask(
          IJobListService jobListService,
          ILogger<JobListTask> logger,
          IEnvironmentConfigurationProvider configurationProvider)
        {
            this._jobListService = jobListService;
            this._logger = logger;
            this._configurationProvider = configurationProvider;
        }

        public async Task Execute(JobTaskOptions args)
        {
            EnvironmentConfiguration configurationAsync = await this._configurationProvider.GetEnvironmentConfigurationAsync(args.Config, args.EnvironmentName);
            Stopwatch stopwatch = Stopwatch.StartNew();
            List<string> list = (await this._jobListService.GetJobListAsync(configurationAsync).ConfigureAwait(false)).ToList<string>();
            this._logger.LogTrace(string.Format("Jobs List: Loaded in {0}ms ({1} jobs).", (object)stopwatch.ElapsedMilliseconds, (object)list.Count));
            ColorLogExtensions.LogConsoleInformation((ILogger)this._logger, $"Jobs list: (Count:{list.Count})", new ConsoleColor?(ConsoleColor.Yellow), new ConsoleColor?());
            foreach (string str in list)
                ColorLogExtensions.LogConsoleInformation((ILogger)this._logger, str, new ConsoleColor?(ConsoleColor.Green), new ConsoleColor?());
            stopwatch = (Stopwatch)null;
        }
    }
}
