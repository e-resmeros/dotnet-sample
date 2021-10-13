using System.Threading.Tasks;

namespace api.Data
{
    public interface IUserDeviceRepoWrapper
    {
        IUserDeviceRepo UserDevice { get; }

        IDeviceRepo Device { get; }

        IUserRepo User { get; }
        
        Task Save();
    }
}