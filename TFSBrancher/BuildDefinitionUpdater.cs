using Microsoft.TeamFoundation.Build.Client;
using System.Text.RegularExpressions;

namespace TFSBrancher
{
    class BuildDefinitionUpdater
    {
        private readonly string newBuildName;
        private IBuildDefinition build;
        private IInfo info;
        private string pattern;

        public BuildDefinitionUpdater(IBuildDefinition buildToUpdate, IInfo info, string newName)
        {
            build = buildToUpdate;
            newBuildName = newName;
            this.info = info;
            pattern = @"\$/"+ this.info.Project.ToString() +@"/DEV";
        }

        public IBuildDefinition UpdateBuildDef()
        {
            UpdateBuildName();
            UpdateProcessParameters();
            ChangeMappingPath();

            return build;
        }

        private void UpdateBuildName()
        {
            build.Name = newBuildName;
        }

        private void UpdateProcessParameters()
        {
            var replacement = info.BaseFolder + info.BranchName;
            Regex rgx = new Regex(pattern);
            var result = rgx.Replace(build.ProcessParameters, replacement);
            result = result.Replace("DevInstall", info.InstallName);
            result = result.Replace("DevCompile", info.CompileName);

            build.ProcessParameters = result;
        }

        private void ChangeMappingPath()
        {
            var replacement = info.BaseFolder + info.BranchName;
            Regex rgx = new Regex(pattern);

            foreach (var mapping in build.Workspace.Mappings)
            {
                if (mapping.ServerItem.ToString().Contains(info.TrunkFolder))
                {
                    var result = rgx.Replace(mapping.ServerItem.ToString(), replacement);
                    mapping.ServerItem = result;
                }
            }

        }
    }
}
