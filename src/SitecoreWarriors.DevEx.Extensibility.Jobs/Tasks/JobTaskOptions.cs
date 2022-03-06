using Sitecore.DevEx.Client.Tasks;

namespace SitecoreWarriors.DevEx.Extensibility.Jobs.Tasks
{
    public class JobTaskOptions : TaskOptionsBase
    {
        public string Config { get; set; }

        public string EnvironmentName { get; set; }

        public bool Verbose { get; set; }

        public bool Trace { get; set; }

        public override void Validate()
        {
            this.Require("Config");
            this.Default("EnvironmentName", (object)"default");
        }
    }
}
