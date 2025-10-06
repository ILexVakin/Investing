using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Investing.Services.Interfaces
{
    public interface IIconCompany
    {
        Task<byte[]> GetIconCompany(string isin);
        Task<Dictionary<string, byte[]>> GetIconFromRedis(string isin);
        Task<Dictionary<string, byte[]>> GetIconFromPg(string isin);

    }
}   

       