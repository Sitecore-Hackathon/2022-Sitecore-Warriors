using Sitecore.DevEx.Configuration.Models;
using System.Threading.Tasks;

namespace SitecoreWarriors.DevEx.Extensibility.Jobs.Tasks
{
  public interface IEnvironmentConfigurationProvider
  {
    Task<EnvironmentConfiguration> GetEnvironmentConfigurationAsync(
      string currentDirectory,
      string environmentName);
  }
}
