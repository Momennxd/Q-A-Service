using ExternalAuthentication.DateToSend;
using ExternalAuthentication.Interfaces;

namespace ExternalAuthentication.Concrete
{
    public class GoogleAuthService : IExternalAuthProvider
    {
        public string ProviderName => "Google";
        public async Task<ExternalAuthResults> AuthenticateGoogleAsync(string idToken)
        {
            throw new NotImplementedException("Google authentication is not implemented yet.");
        }
    }


}
