using System.Diagnostics;

namespace API_Layer.ChatOps
{
    public class CpuUsageMonitor
    {
        private TimeSpan _lastTotalProcessorTime;
        private DateTime _lastCheckTime;

        private readonly Process _process;

        public CpuUsageMonitor()
        {
            _process = Process.GetCurrentProcess();
            _lastTotalProcessorTime = _process.TotalProcessorTime;
            _lastCheckTime = DateTime.UtcNow;
        }

        public double GetCpuUsagePercentage()
        {
            var currentTotalProcessorTime = _process.TotalProcessorTime;
            var currentTime = DateTime.UtcNow;

            var cpuUsedMs = (currentTotalProcessorTime - _lastTotalProcessorTime).TotalMilliseconds;
            var totalMsPassed = (currentTime - _lastCheckTime).TotalMilliseconds;

            _lastTotalProcessorTime = currentTotalProcessorTime;
            _lastCheckTime = currentTime;

            var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);

            return cpuUsageTotal * 100;
        }
    }
}
