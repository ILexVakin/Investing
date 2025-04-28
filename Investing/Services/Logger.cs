using Serilog;
using Serilog.Sinks.Grafana.Loki;

namespace Investing.Services
{
    public static class Logger
    {
        public static ILogger BaseLogger()
        {
             return new LoggerConfiguration()
            .WriteTo.GrafanaLoki("http://localhost:3100")
            .CreateLogger();
        }

    }
}
