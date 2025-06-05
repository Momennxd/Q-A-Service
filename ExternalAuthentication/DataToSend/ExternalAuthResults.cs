namespace ExternalAuthentication.DataToSend
{
    public class ExternalAuthResults
    {
        public bool IsSuccess { get; set; }
        public string ProviderUserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
