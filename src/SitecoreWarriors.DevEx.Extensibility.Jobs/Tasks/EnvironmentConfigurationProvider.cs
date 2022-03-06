using Sitecore.DevEx.Configuration;
using Sitecore.DevEx.Configuration.Models;
using System.Threading.Tasks;

namespace SitecoreWarriors.DevEx.Extensibility.Jobs.Tasks
{
    public class EnvironmentConfigurationProvider : IEnvironmentConfigurationProvider
    {
        private readonly IRootConfigurationManager _rootConfigurationManager;

        public EnvironmentConfigurationProvider(IRootConfigurationManager rootConfigurationManager) => this._rootConfigurationManager = rootConfigurationManager;

        public async Task<EnvironmentConfiguration> GetEnvironmentConfigurationAsync(
          string currentDirectory,
          string environmentName)
        {
            EnvironmentConfiguration configurationAsync;

            if (!(await this._rootConfigurationManager.ResolveRootConfiguration(currentDirectory)).Environments.TryGetValue(environmentName, out configurationAsync))
                throw new InvalidConfigurationException("Environment " + environmentName + " was not defined. Use the login command to define it.");
            return configurationAsync;
        }
    }
}
