using GraphQL.Common.Request;
using Sitecore.DevEx.Configuration.Models;
using Sitecore.DevEx.Serialization.Client.Datasources.Sc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SitecoreWarriors.DevEx.Jobs.Client.Services
{
    public class StartJobService : IStartJobService
    {
        private readonly ISitecoreApiClient _apiClient;

        public StartJobService(ISitecoreApiClient apiClient) => this._apiClient = apiClient;

        public async Task<string> StartJobAsync(
          EnvironmentConfiguration configuration,
         string jobName,
          CancellationToken cancellationToken = default(CancellationToken))
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                {
                    nameof (jobName),
                    jobName
                }
            };

            ISitecoreApiClient apiClient = this._apiClient;
            if (apiClient.Endpoint == null)
            {
                apiClient.Endpoint = configuration;
            }
            return await this._apiClient.RunQuery<string>("/sitecore/api/management", new GraphQLRequest()
            {
                Query = "mutation ($jobName: String!) {\nstartJob(jobName:$jobName)\n}",
                Variables = dictionary
            }, "startJob", cancellationToken);
        }
    }
}
