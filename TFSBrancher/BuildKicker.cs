using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSBrancher
{
    class BuildKicker
    {
        IBuildDefinition buildDef;
        IBuildServer buildServer;
        public BuildKicker(Uri tfsUri, IBuildDefinition buildDef)
        {
            this.buildDef = buildDef;
            var projectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(tfsUri);
            buildServer = projectCollection.GetService(typeof(IBuildServer)) as IBuildServer;
        }

        public void QueueBuild()
        {
            IBuildRequest req = buildDef.CreateBuildRequest();
            req.GetOption = GetOption.LatestOnQueue;

            buildServer.QueueBuild(req);
        }
    }
}
