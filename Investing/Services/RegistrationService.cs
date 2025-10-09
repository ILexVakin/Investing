using Investing.Controllers;
using Investing.Models;
using Investing.Redis;
using Investing.Services.Interfaces;
using Investing.Services.MoexData;
using Microsoft.Extensions.DependencyInjection;

namespace Investing.Services
{
    public static class RegistrationService
    {
        public static IServiceCollection AddRegistrationServices(this IServiceCollection services)
        {
            services.AddScoped<ReadingMoexData>();
            services.AddScoped<ISearchExchangeInstrumentsService, SearchExchangeInstrumentsService>();
            services.AddScoped<IFullModelInstrumentsMoex, FullModelInstrumentsMoex>();
            services.AddScoped<IIconCompany, IconCompany>();
            services.AddScoped<IDetailInstrument<Stocks>, DetailInstrument<Stocks>>();
            services.AddScoped<ICandlesHistory<Stocks>, CandlesHistory<Stocks>>();
            services.AddScoped<IMainOperationRedis, MainOperationRedis>();
            return services;
        }
    }
}
