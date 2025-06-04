using ExternalAuthentication.DateToSend;
using ExternalAuthentication.Interfaces;
using ExternalAuthentication.Options;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace ExternalAuthentication.Concrete
{
    public class GoogleAuthService : IExternalAuthProvider
    {
        private readonly GoogleAuthSettings _settings;

        public GoogleAuthService(IOptions<GoogleAuthSettings> options)
        {
            _settings = options.Value;
        }

        public string ProviderName => "Google";

        public async Task<ExternalAuthResults> AuthenticateAsync(string idToken)
        {
            try
            {
                var validationSettings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _settings.ClientId }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, validationSettings);

                return new ExternalAuthResults
                {
                    IsSuccess = true,
                    ProviderUserId = payload.Subject,
                    Email = payload.Email,
                    FullName = payload.Name
                };
            }
            catch (InvalidJwtException ex)
            {
                return new ExternalAuthResults
                {
                    IsSuccess = false,
                    ErrorMessage = $"Invalid Google token: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new ExternalAuthResults
                {
                    IsSuccess = false,
                    ErrorMessage = $"Unexpected error: {ex.Message}"
                };
            }
        }
    }
}
