using GraphQL.Types;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Jobs;
using Sitecore.Tasks;
using Sitecore.Services.GraphQL.Schemas;
using SitecoreWarriors.DevEx.GraphQL.Jobs.Models;
using System.Collections.Generic;

namespace SitecoreWarriors.DevEx.GraphQL.Jobs.GraphQL.Mutations
{
    internal class StartJobMutation :
      RootFieldType<StringGraphType, string>
    {
        private const string SCHEDULED_PATH = "/sitecore/system/Tasks/Schedules//*[@@templatename='Schedule']";
        private const string DATABASE_MASTER = "master";

        public StartJobMutation()
          : base("startJob", "Start a DB Task Schedule.")
        {
            QueryArgument[] queryArgumentArray = new QueryArgument[1];
            QueryArgument<StringGraphType> queryArgument = new QueryArgument<StringGraphType>();
            queryArgument.Name = "jobName";
            queryArgument.Description = "Name of the job to be started on demand.";
            queryArgumentArray[0] = (QueryArgument)queryArgument;
            this.Arguments = new QueryArguments(queryArgumentArray);
        }

        protected override string Resolve(
          ResolveFieldContext context)
        {
            string jobName = context.GetArgument<string>("jobName");

            if (IfDbTaskExecute(jobName))
            {
                return $"The job {jobName} has been started.";
            }
            else
            {
                return $"There is no DB Task Job with name {jobName}";
            }
        }

        private bool IfDbTaskExecute(string jobName)
        {
            using (new DatabaseSwitcher(Database.GetDatabase(DATABASE_MASTER)))
            {
                foreach (ScheduleItem scheduleItem in Sitecore.Context.Database.SelectItems(SCHEDULED_PATH))
                {
                    if (scheduleItem.Name == jobName)
                    {
                        scheduleItem.Execute();
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
