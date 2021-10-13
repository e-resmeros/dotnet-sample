using System.Threading.Tasks;

namespace api.Data
{
    public interface IRepositoryWrapper
    {
        IQRDataRepo QRData { get; }
        
        Task SaveAync();
    }
}