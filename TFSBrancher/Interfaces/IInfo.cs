
namespace TFSBrancher
{
    interface IInfo
    {
        string FeatureName { get; }
        string BaseFolder { get; }
        string BranchName { get; }
        string CompileName { get; }
        string InstallName { get; }
        string TrunkFolder { get; }
        string TicketNumber { get; }
        string Project { get; }
    }
}
