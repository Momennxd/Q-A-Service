using API_Layer.ChatOps; 
using Prometheus;
using System.Diagnostics;

public class SystemMetricsService : IHostedService, IDisposable
{
    private readonly ILogger<SystemMetricsService> _logger;
    private readonly CpuUsageMonitor _cpuMonitor;
    private Timer? _timer;

    // Define the Prometheus gauges here. They are created once and updated by the timer.
    private readonly Gauge _memoryGauge = Metrics.CreateGauge(
        "process_private_memory_bytes",
        "The private memory used by the application process in bytes."
    );

    private readonly Gauge _cpuGauge = Metrics.CreateGauge(
        "process_cpu_usage_percent",
        "The CPU usage of the application process as a percentage of total machine CPU."
    );

    // We inject the singleton CpuUsageMonitor via Dependency Injection.
    public SystemMetricsService(ILogger<SystemMetricsService> logger, CpuUsageMonitor cpuMonitor)
    {
        _logger = logger;
        _cpuMonitor = cpuMonitor;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("System Metrics Service is starting.");

        // Start a timer that will call CollectMetrics every 5 seconds.
        // The first call will be after a 5-second delay to allow the app to stabilize.
        _timer = new Timer(CollectMetrics, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));

        return Task.CompletedTask;
    }

    private void CollectMetrics(object? state)
    {
        try
        {
            // 1. Collect Memory Usage
            _memoryGauge.Set(Process.GetCurrentProcess().PrivateMemorySize64);

            // 2. Collect CPU Usage using your class
            var cpuUsage = _cpuMonitor.GetCpuUsagePercentage();
            _cpuGauge.Set(cpuUsage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error collecting system metrics.");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("System Metrics Service is stopping.");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}