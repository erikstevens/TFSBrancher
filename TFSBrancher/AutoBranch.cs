using Microsoft.TeamFoundation.Build.Client;
using System;


namespace TFSBrancher
{
    class AutoBranch
    {
        private Uri tfsUri;
        private IInfo info;

        public AutoBranch(Uri tfsUri, IInfo branchInfo)
        {
            info = branchInfo;
            this.tfsUri = tfsUri;
        }

        public void CreateBranch()
        {
            var branching = new Brancher(tfsUri);
            branching.CreateNewBranch(info.TrunkFolder, info.BaseFolder + info.FeatureName, "Erik AutoMagic Branching");
        }

        public void CreateBuildDefinition(string originalBuild, string newBranchName)
        {
            var newBuildDef = CloneBuildDefinition(originalBuild, newBranchName);
            UpdateBuildDefinition(newBuildDef, newBranchName).Save();
        }

        private IBuildDefinition CloneBuildDefinition(string originalBuild, string newBranchName)
        {
            var br = new BuildRetreiver(tfsUri);
            var build = br.GetDefinition(info.Project, originalBuild);
            var vat = new BuildDefinitionCloner(tfsUri, info.Project, newBranchName);
            return vat.CloneBuildDefinition(info.Project, build);
        }

        private IBuildDefinition UpdateBuildDefinition(IBuildDefinition newBuildDef, string newBranchName)
        {
            var defUpdater = new BuildDefinitionUpdater(newBuildDef, info, newBranchName);
            return defUpdater.UpdateBuildDef();
        }
    }
}
