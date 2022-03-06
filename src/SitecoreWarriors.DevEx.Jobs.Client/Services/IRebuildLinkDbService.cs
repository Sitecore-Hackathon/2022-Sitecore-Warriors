using Sitecore.DevEx.Configuration.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SitecoreWarriors.DevEx.Jobs.Client.Services
{
    public interface IRebuildLinkDbService
    {
        Task<string> RebuildLinkDbStartJobAsync(
          EnvironmentConfiguration configuration,
         string jobName,
          CancellationToken cancellationToken = default(CancellationToken));
    }
}
