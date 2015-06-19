using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TFSBrancher
{
    class BuildRetreiver
    {
        private readonly TfsConfigurationServer configurationServer;
        private TfsTeamProjectCollection tfsPC;
        private IBuildServer buildServer;

        public BuildRetreiver(Uri tfsServer)
        {
            configurationServer = TfsConfigurationServerFactory.GetConfigurationServer(tfsServer);

            var collectionNodes = configurationServer.CatalogNode.QueryChildren(
                new[] { CatalogResourceTypes.ProjectCollection },
                false, CatalogQueryOptions.None);
            var collectionId = new Guid(collectionNodes.First().Resource.Properties["InstanceId"]);

            tfsPC = configurationServer.GetTeamProjectCollection(collectionId);
            tfsPC.Authenticate();
            buildServer = (IBuildServer)tfsPC.GetService(typeof(IBuildServer));
        }

        public IBuildDefinition GetDefinition(string projectName, string definitionName)
        {
            var buildDefinitionList = new List<IBuildDefinition>(buildServer.QueryBuildDefinitions(projectName));
            var y = buildDefinitionList.First(x => x.Name == definitionName);
            return y;
        }
    }
}
