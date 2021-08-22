using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using TradingEngineServer.Core.Configuration;

namespace TradingEngineServer.Core
{
    public sealed class TradingEngineServerHostBuilder
    {
        public static IHost BuildTradingEngineServer()
            => Host.CreateDefaultBuilder().ConfigureServices((hostContext, services)
                =>
            {
                // Start with configurations.
                services.AddOptions();
                services.Configure<TradingEngineServerConfiguration>(hostContext.Configuration.GetSection(nameof(TradingEngineServerConfiguration)));

                // Add singleton objects.
                services.AddSingleton<ITradingEngineServer, TradingEngineServer>();

                // Add hosted service.
                services.AddHostedService<TradingEngineServer>();
            }).Build();
    }
}