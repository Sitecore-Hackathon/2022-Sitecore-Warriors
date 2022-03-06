using Sitecore.DevEx.Configuration.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SitecoreWarriors.DevEx.Jobs.Client.Services
{
    public interface IStartJobService
    {
        Task<string> StartJobAsync(
          EnvironmentConfiguration configuration,
         string jobName,
          CancellationToken cancellationToken = default(CancellationToken));
    }
}
