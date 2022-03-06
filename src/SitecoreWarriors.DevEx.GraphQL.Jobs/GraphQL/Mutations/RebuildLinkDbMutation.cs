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
    internal class RebuildLinkDbMutation :
      RootFieldType<StringGraphType, string>
    {
        public RebuildLinkDbMutation()
          : base("rebuildLinkDb", "Start rebuilding a link db.")
        {
            QueryArgument[] queryArgumentArray = new QueryArgument[1];
            QueryArgument<StringGraphType> queryArgument = new QueryArgument<StringGraphType>();
            queryArgument.Name = "dbName";
            queryArgument.Description = "Name of the database to be rebuilt (link)";
            queryArgumentArray[0] = (QueryArgument)queryArgument;
            this.Arguments = new QueryArguments(queryArgumentArray);
        }

        protected override string Resolve(
          ResolveFieldContext context)
        {
            List<string> availableDbNames = new List<string> { "master", "web" };
            string dbName = context.GetArgument<string>("dbName");
            if (availableDbNames.Contains(dbName))
            {
                //It will be good if we get the link DB list from Sitecore API

                Sitecore.Globals.LinkDatabase.Rebuild(Sitecore.Configuration.Factory.GetDatabase(dbName));
                return $"Rebuilding the Link database for {dbName} datbase has started.";
            }
            else
            {
                return $"Provided DB name is not correct. Use master or web.";
            }
        }

    }
}
