using System;
using System.Configuration;

namespace TFSBrancher
{
    class Program
    {
        private static void Main(string[] args)
        {
            var tfsUri = new Uri(ConfigurationManager.AppSettings["TFSServer"]);
            var BranchingInfo = new GetInfo();
            var AutoBrancher = new AutoBranch(tfsUri, BranchingInfo);
            if (!string.IsNullOrWhiteSpace(BranchingInfo.BranchName))
            {
                AutoBrancher.CreateBranch();
                AutoBrancher.CreateBuildDefinition("DevCompile", BranchingInfo.CompileName);
                AutoBrancher.CreateBuildDefinition("DevInstall", BranchingInfo.InstallName);
            }
            var br = new BuildRetreiver(tfsUri);
            var compileBranch = br.GetDefinition(BranchingInfo.Project, BranchingInfo.CompileName);
            var buildStarter = new BuildKicker(tfsUri, compileBranch);
            buildStarter.QueueBuild();
        }
    }
}
