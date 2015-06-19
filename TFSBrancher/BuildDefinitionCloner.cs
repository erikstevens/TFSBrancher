using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Common;

namespace TFSBrancher
{
    class BuildDefinitionCloner
    {
        private readonly TfsConfigurationServer configurationServer;
        private readonly string newBuildName;
        private IBuildDefinition newBuild;
        private TfsTeamProjectCollection tfsPC;
        private IBuildServer buildServer;

        public BuildDefinitionCloner(Uri tfsServer, string project, string newName)
        {
            configurationServer = TfsConfigurationServerFactory.GetConfigurationServer(tfsServer);
            
            var collectionNodes = configurationServer.CatalogNode.QueryChildren(
                new[] { CatalogResourceTypes.ProjectCollection },
                false, CatalogQueryOptions.None);
            var collectionId = new Guid(collectionNodes.First().Resource.Properties["InstanceId"]);

            tfsPC = configurationServer.GetTeamProjectCollection(collectionId);
            tfsPC.Authenticate();
            buildServer = (IBuildServer)tfsPC.GetService(typeof(IBuildServer));

            newBuildName = newName;
        }

        public IBuildDefinition CloneBuildDefinition(string projectName, IBuildDefinition originalBuild)
        {
            newBuild = buildServer.CreateBuildDefinition(projectName);
            newBuild.CopyFrom(originalBuild);
            
            return newBuild;
        }

        
    }
}
