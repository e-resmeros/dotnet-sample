using System.Collections.Generic;
using System.Threading.Tasks;
using api.Core.Data;
using api.Models;

namespace api.Data
{
    public interface IUserDeviceRepo : IRepositoryBase<UserDevice>
    {
        public Task<IEnumerable<UserDevice>> GetUserDevices();

        public Task SaveAsync(); 

    }
}