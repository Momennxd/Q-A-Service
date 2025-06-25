using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace API_Layer.ChatOps
{
    public class MetricsParser
    {
        private static readonly Regex RegexRequest = new Regex(
            @"^http_requests_received_total\{.*endpoint=""([^""]*)""[^}]*\}\s+(\d+)",
            RegexOptions.Compiled);

        // CPU time in seconds (sum of user + system)
        private static readonly Regex RegexCpuSecondsTotal = new Regex(
            @"^process_cpu_seconds_total\s+(\d+\.?\d*)",
            RegexOptions.Compiled);

        // Memory usage bytes (using working set as an example)
        private static readonly Regex RegexMemoryUsage = new Regex(
            @"^process_working_set_bytes\s+(\d+)",
            RegexOptions.Compiled);

        public string ParseAndSummarize(string rawMetrics)
        {
            if (string.IsNullOrWhiteSpace(rawMetrics))
                return "No metrics data available.";

            var endpointCounts = new Dictionary<string, long>();
            double? cpuSecondsTotal = null; // Use nullable types to track if they've been found
            long? memoryUsageBytes = null;

            using (var reader = new StringReader(rawMetrics))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // Use an if/else-if structure for efficiency and clarity
                    var reqMatch = RegexRequest.Match(line);
                    if (reqMatch.Success)
                    {
                        var endpoint = reqMatch.Groups[1].Value;
                        if (string.IsNullOrWhiteSpace(endpoint))
                            endpoint = "root";

                        if (long.TryParse(reqMatch.Groups[2].Value, out long count))
                        {
                            endpointCounts.TryGetValue(endpoint, out long currentCount);
                            endpointCounts[endpoint] = currentCount + count;
                        }
                    }
                    // UPDATED: Only parse CPU time if it hasn't been found yet
                    else if (!cpuSecondsTotal.HasValue)
                    {
                        var cpuMatch = RegexCpuSecondsTotal.Match(line);
                        if (cpuMatch.Success)
                        {
                            if (double.TryParse(cpuMatch.Groups[1].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out double cpuVal))
                                cpuSecondsTotal = cpuVal;
                        }
                    }
                    // UPDATED: Only parse Memory usage if it hasn't been found yet
                    else if (!memoryUsageBytes.HasValue)
                    {
                        var memMatch = RegexMemoryUsage.Match(line);
                        if (memMatch.Success)
                        {
                            if (long.TryParse(memMatch.Groups[1].Value, out long memVal))
                                memoryUsageBytes = memVal;
                        }
                    }
                }
            }

            var sb = new StringBuilder();
            sb.AppendLine("📊 **Metrics Summary**");
            sb.AppendLine("--------------------");

            long totalRequests = endpointCounts.Values.Sum();
            sb.AppendLine($"Total Requests: {totalRequests:N0}");

            // Use .GetValueOrDefault() for safe access
            sb.AppendLine($"CPU Time Total: {cpuSecondsTotal.GetValueOrDefault():F2} seconds");

            // CORRECTED: The calculation is for Mebibytes (MiB), not Megabytes (MB).
            double memoryMiB = memoryUsageBytes.GetValueOrDefault() / (1024.0 * 1024.0);
            sb.AppendLine($"Memory Usage: {memoryMiB:F2} MiB");

            if (endpointCounts.Any())
            {
                var mostUsedEndpoint = endpointCounts.OrderByDescending(kv => kv.Value).First();
                sb.AppendLine($"Most Used Endpoint: `{mostUsedEndpoint.Key}` ({mostUsedEndpoint.Value:N0} requests)");
            }

            sb.AppendLine();
            sb.AppendLine("📋 **All Endpoint Requests**");
            sb.AppendLine("-------------------------");
            if (endpointCounts.Any())
            {
                foreach (var entry in endpointCounts.OrderByDescending(kv => kv.Value))
                {
                    sb.AppendLine($"  - `{entry.Key}`: {entry.Value:N0} requests");
                }
            }
            else
            {
                sb.AppendLine("  No endpoint request data found.");
            }

            return sb.ToString();
        }
    }
}