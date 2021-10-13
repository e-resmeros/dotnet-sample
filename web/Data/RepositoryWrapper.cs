using System.Threading.Tasks;
using api.Config;

namespace api.Data
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private AppDb _db;
        private IQRDataRepo _qrDataRepo;

        public IQRDataRepo QRData
        {
            get {
                if (null == _qrDataRepo)
                {
                    _qrDataRepo = new QRDataRepo(_db);
                }

                return _qrDataRepo;
            }
        }

        public RepositoryWrapper(AppDb db)
        {
            _db = db;
        }

        public async Task SaveAync()
        {
            await _db.SaveChangesAsync();
        }
    }
}