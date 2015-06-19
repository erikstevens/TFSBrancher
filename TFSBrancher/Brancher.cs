using System;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace TFSBrancher
{
    class Brancher
    {
        private VersionControlServer vcs;
        private readonly Uri tfsUri;

        public Brancher(Uri uri)
        {
            tfsUri = uri;
            var projectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(tfsUri);
            vcs = projectCollection.GetService<VersionControlServer>();
        }

        public void CreateNewBranch(string trunkBranch,string fullBranchFolder, string checkinComment)
        {
            vcs.CreateBranch(trunkBranch, fullBranchFolder, VersionSpec.Latest, null, checkinComment, null, null, null);
        }
    }
}
