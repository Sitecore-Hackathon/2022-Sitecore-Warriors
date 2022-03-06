using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace SitecoreWarriors.DevEx.GraphQL.Jobs
{
  [ExcludeFromCodeCoverage]
  public class DependencyConfigurator : IServicesConfigurator
  {
    public void Configure(IServiceCollection serviceCollection)
    {
    }
  }
}
