using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Service.EventBus
{
    public class EventBusListenerWorker : BackgroundService
    {
        private readonly ILogger<EventBusListenerWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public EventBusListenerWorker(IServiceProvider serviceProvider, ILogger<EventBusListenerWorker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
	        EventBusFactory.UseEventBusListener(_serviceProvider);

			await Task.Delay(Timeout.Infinite, stoppingToken);
            _logger.LogDebug($"EventBusListener is stopping.");
        }
    }
}

