using GraphQL.Common.Request;
using Sitecore.DevEx.Configuration.Models;
using Sitecore.DevEx.Core.Client.GraphQl;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SitecoreWarriors.DevEx.Jobs.Client.Services
{
    public class JobListService : IJobListService
    {
        private readonly ISitecoreApiClient _apiClient;

        public JobListService(ISitecoreApiClient apiClient) => this._apiClient = apiClient;

        public Task<IEnumerable<string>> GetJobListAsync(
          EnvironmentConfiguration configuration,
          CancellationToken cancellationToken = default(CancellationToken))
        {
            ISitecoreApiClient apiClient = this._apiClient;
            if (apiClient.Endpoint == null)
            {
                apiClient.Endpoint = configuration;
            }
            return this._apiClient.RunQuery<IEnumerable<string>>("/sitecore/api/management", new GraphQLRequest()
            {
                Query = "\nquery{\n  jobsList\n}",
                Variables = (object)new Dictionary<string, object>()
            }, "jobsList", cancellationToken);
        }
    }
}
