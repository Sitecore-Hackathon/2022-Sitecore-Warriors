using Sitecore.DevEx.Configuration.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SitecoreWarriors.DevEx.Jobs.Client.Services
{
  public interface IJobListService
  {
    Task<IEnumerable<string>> GetJobListAsync(
      EnvironmentConfiguration configuration,
      CancellationToken cancellationToken = default (CancellationToken));
  }
}
