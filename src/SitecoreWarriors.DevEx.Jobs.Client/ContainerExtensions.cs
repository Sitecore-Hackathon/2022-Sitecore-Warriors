using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SitecoreWarriors.DevEx.Jobs.Client.Services;
using System.Diagnostics.CodeAnalysis;

namespace SitecoreWarriors.DevEx.Jobs.Client
{
    [ExcludeFromCodeCoverage]
    public static class ContainerExtensions
    {
        public static IServiceCollection AddJobServices(
          this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddTransient<IJobListService, JobListService>();
            serviceCollection.TryAddTransient<IStartJobService, StartJobService>();
            serviceCollection.TryAddTransient<IRebuildLinkDbService, RebuildLinkDbService>();
            return serviceCollection;
        }
    }
}
