using Ombi.Core.Processor;

namespace Ombi.Core.Models.UI
{
    public class UpdateViewModel : UpdateModel
    {
        public string InstalledVersion { get; set; }
        public bool UpdateAvailable => InstalledVersion != UpdateVersionString;
    }
}