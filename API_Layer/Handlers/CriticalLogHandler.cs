namespace API_Layer.Handlers
{
    public class CriticalLogHandler
    {
        public event Action<string> OnCriticalLog;

        public void HandleCritical(string message)
        {
            var timeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            var messageWithTime = $"[{timeStamp}] {message}";
            OnCriticalLog?.Invoke(messageWithTime) ;
        }
    }
}
