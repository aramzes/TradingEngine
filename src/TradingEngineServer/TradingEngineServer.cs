using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TradingEngineServer.Core.Configuration;
using TradingEngineServer.Logging;

namespace TradingEngineServer.Core
{
    class TradingEngineServer : BackgroundService, ITradingEngineServer
    {
        private readonly IOptions<TradingEngineServerConfiguration> _engineConfiguration;
        private readonly ITextLogger _logger;

        public TradingEngineServer(IOptions<TradingEngineServerConfiguration> engineConfiguration,
            ITextLogger logger)
        {
            _engineConfiguration = engineConfiguration ?? throw new ArgumentNullException(nameof(engineConfiguration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Run(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        // public Task Run(CancellationToken token)

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.Information(nameof(TradingEngineServer), "Starting Trading Engine Server");
            while (!stoppingToken.IsCancellationRequested)
            { }
            _logger.Information(nameof(TradingEngineServer), "Stopping Trading Engine Server");
            return Task.CompletedTask;
        }
    }
}