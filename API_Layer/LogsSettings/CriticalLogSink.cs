using API_Layer.Handlers;
using Serilog.Core;
using Serilog.Events;

namespace API_Layer.LogsSettings
{

    public class CriticalLogSink : ILogEventSink
    {
        private readonly CriticalLogHandler _handler;

        public CriticalLogSink(CriticalLogHandler handler)
        {
            _handler = handler;
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent.Level == LogEventLevel.Fatal)
            {
                var message = logEvent.RenderMessage();
                _handler.HandleCritical(message);
            }
        }
    }
}
