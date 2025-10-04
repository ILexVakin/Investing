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
            services.AddScoped<IMainOperationRedis<string>, MainOperationRedis>();
            return services;
        }
    }
}
