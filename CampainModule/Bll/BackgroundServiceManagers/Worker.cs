using CampainModule.Bll.Services;

namespace CampainModule.Bll.BackgroundServiceManagers
{
    public class Worker : BackgroundService
    {
        ILogger<Worker> _logger;
        public static int _time;
        private readonly IRunCampaignService _runCampaignService;
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(1),stoppingToken);
            }
        }
    }
}
