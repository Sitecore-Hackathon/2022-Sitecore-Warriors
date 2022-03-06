using GraphQL.Types;
using Sitecore.ContentSearch;
using Sitecore.Services.GraphQL.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using System.Reflection;
using Sitecore;
using Sitecore.Collections;
using System.Text;
using Sitecore.Jobs;
using Sitecore.Data.Items;
using Sitecore.Data;
using Sitecore.Data.Fields;

namespace SitecoreWarriors.DevEx.GraphQL.Jobs.GraphQL.Queries
{
    internal class JobsListQuery :
      RootFieldType<ListGraphType<StringGraphType>, IEnumerable<string>>
    {
        private const string SCHEDULED_PATH = "/sitecore/system/Tasks/Schedules//*[@@templatename='Schedule']";
        private const string DATABASE_MASTER = "master";
        private readonly DateTime now = DateTime.Now;
        
        public JobsListQuery()
          : base("jobsList", "Get all jobs.")
        {
        }

        protected override IEnumerable<string> Resolve(ResolveFieldContext context)
        {
            var jobs = JobManager.GetJobs();
            List<string> results = new List<string>();

            BaseJobManager requiredService = ServiceProviderServiceExtensions.GetRequiredService<BaseJobManager>(ServiceLocator.ServiceProvider);

            if (requiredService == null || !(requiredService is DefaultJobManager defaultJobManager))
            {
                Sitecore.Diagnostics.Log.Warn("JobListQuery: BaseJobManager service is not available.", this);
                return new List<string>() { "Error while retriving the jobs list." };
            }
            else
            {
                this.GetJobs(jobs, ref results);
                this.RetrieveDatabaseTasks(ref results);
            }

            return results;
        }

        protected virtual void RetrieveDatabaseTasks(ref List<string> results)
        {
            using (new DatabaseSwitcher(Database.GetDatabase(DATABASE_MASTER)))
            {
                foreach (Item scheduleItem in Sitecore.Context.Database.SelectItems(SCHEDULED_PATH))
                {
                    results.Add($"(DbTaskSchedule) {scheduleItem.Name} - (LastRun: {this.GetDateOrEmpty(scheduleItem.Fields["Last run"])})");
                }
            }
        }

        protected virtual void GetJobs(ICollection<BaseJob> enumerable, ref List<string> results)
        {
            if (enumerable.Count > 0)
            {
                StringBuilder stringBuilder = new StringBuilder();
                int num = 1;
                foreach (BaseJob baseJob in (IEnumerable<BaseJob>)enumerable)
                {
                    TimeSpan timeSpan = this.now - baseJob.QueueTime.ToLocalTime();
                    string str1 = timeSpan.Hours == 0 ? string.Empty : timeSpan.Hours.ToString() + "h ";
                    string str2 = timeSpan.Minutes == 0 ? string.Empty : timeSpan.Minutes.ToString() + "m ";

                    results.Add($"{baseJob.Name} - (Priority: {baseJob.Options.Priority}) (Status: {((DefaultJob)baseJob).Status.State}) (QueueTime: {((DefaultJob)baseJob).QueueTime.ToString()}) (Category: {baseJob.Category})");
                    ++num;
                }
            }
        }

        /// <summary>
        /// Get Date Field
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        private string GetDateOrEmpty(Field field)
        {
            DateField field2 = field;
            if (field.Value == string.Empty)
            {
                return string.Empty;
            }
            return field2.DateTime.ToString("dd/MM/yyyy HH:mm:ss");
        }
    }
}
