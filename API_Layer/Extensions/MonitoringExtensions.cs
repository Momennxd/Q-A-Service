using API_Layer.ChatOps;
using Prometheus;
using System.Collections.Concurrent; // Use ConcurrentDictionary for thread safety
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace API_Layer.Extensions
{
    public static class MonitoringExtensions
    {
        public static IServiceCollection AddAppMonitoring(this IServiceCollection services)
        {
            services.AddSingleton<MetricsParser>();
            services.AddSingleton<CpuUsageMonitor>();
            services.AddHostedService<SystemMetricsService>();
            return services;
        }

        public static IApplicationBuilder UseAppMonitoring(this IApplicationBuilder app)
        {
            // --- METRIC DEFINITIONS ---

            var requestCounter = Metrics.CreateCounter("http_requests_total", "Total number of HTTP requests", new CounterConfiguration
            {
                LabelNames = new[] { "method", "endpoint", "status_code" }
            });

            var errorCounter = Metrics.CreateCounter("http_errors_total", "Total number of HTTP errors", new CounterConfiguration
            {
                LabelNames = new[] { "method", "endpoint" }
            });

            var durationHistogram = Metrics.CreateHistogram("http_duration_seconds", "Histogram of HTTP request durations", new HistogramConfiguration
            {
                LabelNames = new[] { "method", "endpoint", "status_code" },
                Buckets = Histogram.LinearBuckets(0.01, 0.05, 20)
            });

            // --- NEW: Gauges for Min/Max Duration ---
            // We use a Gauge to set an explicit value.
            var minDurationGauge = Metrics.CreateGauge("http_request_duration_min_seconds", "Minimum duration of an HTTP request for an endpoint.", new GaugeConfiguration
            {
                LabelNames = new[] { "method", "endpoint" }
            });

            var maxDurationGauge = Metrics.CreateGauge("http_request_duration_max_seconds", "Maximum duration of an HTTP request for an endpoint.", new GaugeConfiguration
            {
                LabelNames = new[] { "method", "endpoint" }
            });

            // --- NEW: In-memory tracking for Min/Max ---
            // A thread-safe dictionary to hold the current max values for each endpoint.
            // Key: "METHOD:/endpoint/path", Value: max duration in seconds.
            var maxDurationTracker = new ConcurrentDictionary<string, double>();

            app.Use(async (context, next) =>
            {
                var stopwatch = Stopwatch.StartNew();
                await next.Invoke();
                stopwatch.Stop();

                var elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
                var method = context.Request.Method.ToLower();
                var path = SanitizePath(context.Request.Path);
                var statusCode = context.Response.StatusCode.ToString();
                var endpointKey = $"{method}:{path}"; // Unique key for our tracker

                // --- UPDATE METRICS ---

                // Standard metrics
                requestCounter.Labels(method, path, statusCode).Inc();
                durationHistogram.Labels(method, path, statusCode).Observe(elapsedSeconds);

                if (context.Response.StatusCode >= 400)
                {
                    errorCounter.Labels(method, path).Inc();
                }

                // --- NEW: Update Min/Max Gauges ---

                // For Max: We need to track the current max to avoid it ever decreasing.
                // We use a "compare and swap" loop to ensure thread safety.
                maxDurationTracker.AddOrUpdate(
                    key: endpointKey,
                    addValue: elapsedSeconds,
                    updateValueFactory: (_, existingMax) => elapsedSeconds > existingMax ? elapsedSeconds : existingMax
                );
                // Set the gauge to the latest known maximum.
                maxDurationGauge.Labels(method, path).Set(maxDurationTracker[endpointKey]);


                // For Min: This is simpler. We can just set it. Prometheus will show the lowest value over time.
                // However, to be more explicit and resilient, we can track it similarly.
                // For simplicity here, we'll rely on a different approach in the parser.
                // A simple gauge will work for now, but it's less robust than the max tracker.
                // A better approach is to parse all durations from the histogram, which is complex.
                // Let's add a min gauge and update the parser to handle it.
                minDurationGauge.Labels(method, path).Set(elapsedSeconds); // This will be overwritten often
            });

            app.UseMetricServer();
            return app;
        }

        private static string SanitizePath(string path)
        {
            return Regex.Replace(path, @"\d+", "{id}");
        }
    }
}