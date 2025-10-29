using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace Shared.Logging
{
    public static class LoggingExtensions
    {
        public static IHostBuilder UseSerilogLogger(this IHostBuilder builder)
        {
            builder.UseSerilog((context, services, conf) =>
            {
                var options = context.Configuration
                                     .GetSection(LoggingOptions.SectionName)
                                     .Get<LoggingOptions>() ?? new LoggingOptions();


                var logLevel = Enum.TryParse<LogEventLevel>(options.Level, true, out var level)
                    ? level
                    : LogEventLevel.Information;

                conf.MinimumLevel.Is(logLevel)
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                    .MinimumLevel.Override("System", LogEventLevel.Warning);

                conf.Enrich.FromLogContext() 
                    .Enrich.WithMachineName()
                    .Enrich.WithThreadId()
                    .Enrich.WithProperty("ApplicationName", context.HostingEnvironment.ApplicationName)
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName);

                if (options.UseConsole)
                    conf.WriteTo.Console(new CompactJsonFormatter());

                if (options.UseFile)
                {
                    conf.WriteTo.File(
                        path: options.FilePath,
                        rollingInterval: RollingInterval.Day,
                        rollOnFileSizeLimit: true,
                        fileSizeLimitBytes: 10 * 1024 * 1024,
                        retainedFileCountLimit: 10,
                        formatter: new CompactJsonFormatter()
                    );
                }

                if (options.UseSeq && !string.IsNullOrWhiteSpace(options.SeqUrl))
                {
                    conf.WriteTo.Seq(options.SeqUrl);
                }

                conf.ReadFrom.Configuration(context.Configuration);
            });

            return builder;
        }
    }
}