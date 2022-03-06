using GraphQL.Types;
using Sitecore.Abstractions;
using SitecoreWarriors.DevEx.GraphQL.Jobs.GraphQL.Mutations;
using SitecoreWarriors.DevEx.GraphQL.Jobs.GraphQL.Queries;
using Sitecore.Services.GraphQL.Schemas;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SitecoreWarriors.DevEx.GraphQL.Jobs
{
    [ExcludeFromCodeCoverage]
    internal class JobSchemaProvider : SchemaProviderBase
    {
        public override IEnumerable<FieldType> CreateRootQueries()
        {
            yield return (FieldType)new JobsListQuery();
        }

        public override IEnumerable<FieldType> CreateRootMutations()
        {            
            yield return (FieldType)new StartJobMutation();
            yield return (FieldType)new RebuildLinkDbMutation();
        }
    }
}
