using GraphQL.Common.Request;
using Sitecore.DevEx.Configuration.Models;
using Sitecore.DevEx.Serialization.Client.Datasources.Sc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SitecoreWarriors.DevEx.Jobs.Client.Services
{
    public class RebuildLinkDbService : IRebuildLinkDbService
    {
        private readonly ISitecoreApiClient _apiClient;

        public RebuildLinkDbService(ISitecoreApiClient apiClient) => this._apiClient = apiClient;

        public async Task<string> RebuildLinkDbStartJobAsync(
          EnvironmentConfiguration configuration,
         string dbName,
          CancellationToken cancellationToken = default(CancellationToken))
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                {
                    nameof (dbName),
                    dbName
                }
            };

            ISitecoreApiClient apiClient = this._apiClient;
            if (apiClient.Endpoint == null)
            {
                apiClient.Endpoint = configuration;
            }
            return await this._apiClient.RunQuery<string>("/sitecore/api/management", new GraphQLRequest()
            {
                Query = "mutation ($dbName: String!) {\nrebuildLinkDb(dbName:$dbName)\n}",
                Variables = dictionary
            }, "rebuildLinkDb", cancellationToken);
        }
    }
}
