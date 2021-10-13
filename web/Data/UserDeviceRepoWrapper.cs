using System.Threading.Tasks;
using api.Config;

namespace api.Data
{
    public class UserDeviceRepoWrapper : IUserDeviceRepoWrapper
    {

        private AppDb _db;
        private IUserDeviceRepo _userDevice;
        private IDeviceRepo _device;
        private IUserRepo _user;

        public UserDeviceRepoWrapper(AppDb db)
        {
            _db = db;
        }
        public IUserDeviceRepo UserDevice
        {
            get {
                if(_userDevice == null)
                {
                    _userDevice = new UserDeviceRepo(_db);
                }
                return _userDevice;
            }
        }

        public IDeviceRepo Device
        {
            get {
                if(_device == null)
                {
                    _device = new DeviceRepo(_db);
                }
                return _device;
            }
        }

        public IUserRepo User 
        {
            get { 
                if (_user == null)
                {
                    _user = new UserRepo(_db);
                }
                return _user;
            }
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}