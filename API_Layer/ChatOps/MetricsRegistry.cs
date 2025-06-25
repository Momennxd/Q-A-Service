using Prometheus;

namespace API_Layer.ChatOps
{
    public static class MetricsRegistry
    {
        public static readonly Counter RequestCounter = Metrics.CreateCounter("http_requests_total", "Total HTTP requests",
            new CounterConfiguration
            {
                LabelNames = new[] { "method", "endpoint", "status" }
            });

        public static readonly Counter ErrorCounter = Metrics.CreateCounter("http_requests_errors_total", "Total HTTP error requests",
            new CounterConfiguration
            {
                LabelNames = new[] { "method", "endpoint", "status" }
            });

        public static readonly Histogram ResponseDuration = Metrics.CreateHistogram("http_response_duration_seconds", "HTTP response latency in seconds",
            new HistogramConfiguration
            {
                LabelNames = new[] { "method", "endpoint", "status" },
                Buckets = Histogram.LinearBuckets(0.01, 0.05, 20)
            });

        public static readonly Counter TotalRequests = Metrics.CreateCounter("http_requests_all_total", "Total HTTP requests overall");

        public static readonly Gauge CpuGauge = Metrics.CreateGauge("process_cpu_seconds_total", "Total CPU seconds used by process");
        public static readonly Gauge MemGauge = Metrics.CreateGauge("process_memory_bytes", "Memory usage in bytes");
    }
}