using System.Threading.Tasks;
using Hangfire.Server;

namespace Ombi.Schedule.Jobs.Ombi
{
    public interface IOmbiAutomaticUpdater : IBaseJob
    {
        Task Update(PerformContext context);
        Task<bool> UpdateAvailable(string branch, string currentVersion);
    }
}