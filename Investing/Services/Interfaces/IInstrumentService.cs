using System.Threading.Tasks;

namespace Investing.Services.Interfaces
{
    public interface IInstrumentService
    {
        Task<T> GetAllInstrumentsAsync<T>();
    }
}
