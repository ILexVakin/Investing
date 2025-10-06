using Investing.Models;
using System.Threading.Tasks;
using static Investing.Services.MoexData.FullModelInstrumentsMoex;

namespace Investing.Services.Interfaces
{
    public interface IDetailInstrument<T> where T : class
    {
        Task<T> DetailInstrument(string secId, TypeInstrument typeInstrument);
    }
}
