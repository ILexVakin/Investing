using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Investing.Redis;
using Investing.Redis.Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(context.Configuration.GetConnectionString("Redis")));

        services.AddScoped<IDatabase>(sp =>
                         sp.GetRequiredService<IConnectionMultiplexer>().GetDatabase());

        services.AddScoped<IMainOperationRedis<string>,IconCompany>();

    })
    .Build();
ReadingIcon readingIcon = new ReadingIcon();

var k = readingIcon.GetAllIconsCampany();