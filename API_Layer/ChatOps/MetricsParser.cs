using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace API_Layer.ChatOps
{
    // A simple DTO to hold all metrics for a single endpoint
    public class EndpointMetrics
    {
        public long TotalRequests { get; set; }
        public double MinDurationMs { get; set; } = double.MaxValue; // Initialize to a large value
        public double MaxDurationMs { get; set; } = 0;
    }

    public class MetricsParser
    {
        // Regex to capture endpoint and count from http_requests_total
        private static readonly Regex RegexRequest = new Regex(
            @"^http_requests_total\{[^}]*endpoint=""([^""]+)""[^}]*\}\s+(\d+)",
            RegexOptions.Compiled);

        // Regex for system metrics
        private static readonly Regex RegexCpuUsage = new Regex(@"^process_cpu_usage_percent\s+([0-9]+\.?[0-9]*)", RegexOptions.Compiled);
        private static readonly Regex RegexMemoryUsage = new Regex(@"^process_private_memory_bytes\s+(\d+)", RegexOptions.Compiled);
        private static readonly Regex RegexProcessStartTime = new Regex(@"^process_start_time_seconds\s+([0-9]+\.?[0-9]*)", RegexOptions.Compiled);

        // --- NEW: Regex for Min/Max Duration ---
        private static readonly Regex RegexMinDuration = new Regex(
            @"^http_request_duration_min_seconds\{[^}]*endpoint=""([^""]+)""[^}]*\}\s+([0-9]+\.?[0-9]*)",
            RegexOptions.Compiled);

        private static readonly Regex RegexMaxDuration = new Regex(
            @"^http_request_duration_max_seconds\{[^}]*endpoint=""([^""]+)""[^}]*\}\s+([0-9]+\.?[0-9]*)",
            RegexOptions.Compiled);

        public string ParseAndSummarize(string rawMetrics)
        {
            if (string.IsNullOrWhiteSpace(rawMetrics))
            {
                return "⚠️ No metrics data available to parse.";
            }

            // --- Data Storage ---
            // Use a dictionary with a custom class to hold all metrics per endpoint
            var endpointData = new Dictionary<string, EndpointMetrics>(StringComparer.OrdinalIgnoreCase);
            double? cpuUsagePercent = null;
            long? memoryUsageBytes = null;
            double? processStartTimeSeconds = null;

            using (var reader = new StringReader(rawMetrics))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Helper function to get or create an entry in our dictionary
                    EndpointMetrics GetOrCreateEndpoint(string key)
                    {
                        if (!endpointData.TryGetValue(key, out var metrics))
                        {
                            metrics = new EndpointMetrics();
                            endpointData[key] = metrics;
                        }
                        return metrics;
                    }

                    // 1. Parse Total Requests
                    var reqMatch = RegexRequest.Match(line);
                    if (reqMatch.Success)
                    {
                        var endpoint = reqMatch.Groups[1].Value.Trim();
                        if (long.TryParse(reqMatch.Groups[2].Value, out long count))
                        {
                            GetOrCreateEndpoint(endpoint).TotalRequests += count;
                        }
                        continue;
                    }

                    // --- NEW: Parse Min/Max Durations ---
                    var minMatch = RegexMinDuration.Match(line);
                    if (minMatch.Success)
                    {
                        var endpoint = minMatch.Groups[1].Value.Trim();
                        if (double.TryParse(minMatch.Groups[2].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out double val))
                        {
                            var metrics = GetOrCreateEndpoint(endpoint);
                            // Find the true minimum across all reported values
                            if (val < metrics.MinDurationMs)
                            {
                                metrics.MinDurationMs = val;
                            }
                        }
                        continue;
                    }

                    var maxMatch = RegexMaxDuration.Match(line);
                    if (maxMatch.Success)
                    {
                        var endpoint = maxMatch.Groups[1].Value.Trim();
                        if (double.TryParse(maxMatch.Groups[2].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out double val))
                        {
                            GetOrCreateEndpoint(endpoint).MaxDurationMs = val; // This is already the tracked max
                        }
                        continue;
                    }

                    // Parse system metrics (no change here)
                    if (!cpuUsagePercent.HasValue && RegexCpuUsage.Match(line) is { Success: true } cpuMatch && double.TryParse(cpuMatch.Groups[1].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var cpuVal)) cpuUsagePercent = cpuVal;
                    if (!memoryUsageBytes.HasValue && RegexMemoryUsage.Match(line) is { Success: true } memMatch && long.TryParse(memMatch.Groups[1].Value, out var memVal)) memoryUsageBytes = memVal;
                    if (!processStartTimeSeconds.HasValue && RegexProcessStartTime.Match(line) is { Success: true } startTimeMatch && double.TryParse(startTimeMatch.Groups[1].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var startTimeVal)) processStartTimeSeconds = startTimeVal;
                }
            }

            // --- Formatting the Output ---
            var sb = new StringBuilder();
            sb.AppendLine("📊 **Application Metrics Summary**");
            sb.AppendLine("-------------------------------");

            sb.AppendLine($"🖥️ **CPU Usage:** {cpuUsagePercent.GetValueOrDefault():F2} %");
            double memoryMiB = memoryUsageBytes.GetValueOrDefault() / (1024.0 * 1024.0);
            sb.AppendLine($"🧠 **Memory Usage:** {memoryMiB:F2} MiB");
            sb.AppendLine();

            long totalRequests = endpointData.Values.Sum(e => e.TotalRequests);
            sb.AppendLine($"📈 **Total Requests:** {totalRequests:N0}");

            if (processStartTimeSeconds.HasValue)
            {
                var uptimeSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - processStartTimeSeconds.Value;
                if (uptimeSeconds > 1)
                {
                    sb.AppendLine($"⚡ **Avg. RPS (since start):** {totalRequests / uptimeSeconds:F2}");
                }
            }

            if (endpointData.Any())
            {
                var mostUsedEndpoint = endpointData.OrderByDescending(kv => kv.Value.TotalRequests).First();
                sb.AppendLine($"🎯 **Most Used Endpoint:** `{mostUsedEndpoint.Key}` ({mostUsedEndpoint.Value.TotalRequests:N0} times)");
            }

            sb.AppendLine();
            sb.AppendLine("📋 **Requests per Endpoint (Top 10)**");
            sb.AppendLine("----------------------------------");

            if (endpointData.Any())
            {
                foreach (var entry in endpointData.OrderByDescending(kv => kv.Value.TotalRequests).Take(10))
                {
                    // Convert seconds to milliseconds for readability
                    var minMs = entry.Value.MinDurationMs * 1000;
                    var maxMs = entry.Value.MaxDurationMs * 1000;

                    // Handle case where MinDurationMs was not updated
                    var minText = minMs == double.MaxValue * 1000 ? "N/A" : $"{minMs:F0}ms";

                    sb.AppendLine($"  - `{entry.Key}`: {entry.Value.TotalRequests:N0} requests");
                    sb.AppendLine($"    (min: {minText}, max: {maxMs:F0}ms)");
                }
                if (endpointData.Count > 10)
                {
                    sb.AppendLine($"  ...and {endpointData.Count - 10} more.");
                }
            }
            else
            {
                sb.AppendLine("  No HTTP request data found yet.");
            }

            return sb.ToString();
        }
    }
}