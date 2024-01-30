using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace FaithfulRemindersWeb.Business.Logging
{
    /// <summary>
    /// Serilog Configuration
    /// </summary>
    public static class LoggingConfig
    {
        #region ConfigureLogging
        public static void ConfigureLogging(IServiceCollection services)
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(Path.Combine("Log", "log.txt"), rollingInterval: RollingInterval.Day)
            .CreateLogger();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(logger, dispose: true);
            });
        }
        #endregion
    }
}
