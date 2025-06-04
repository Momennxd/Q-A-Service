using ExternalAuthentication.DataToSend;
using ExternalAuthentication.Interfaces;
using ExternalAuthentication.Options;
using Google.Apis.Auth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace ExternalAuthentication.Concrete
{
    public class GoogleAuthService : IExternalAuthProvider
    {
        private readonly GoogleAuthSettings _settings;
        private readonly ILogger<GoogleAuthService> _logger;

        public GoogleAuthService(IOptions<GoogleAuthSettings> options, ILogger<GoogleAuthService> logger)
        {
            _settings = options.Value;
            _logger = logger;
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
                _logger.LogWarning("Google token validation failed: {Message}", ex.Message);

                return new ExternalAuthResults
                {
                    IsSuccess = false,
                    ErrorMessage = $"Invalid Google token: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during Google authentication");

                return new ExternalAuthResults
                {
                    IsSuccess = false,
                    ErrorMessage = $"Unexpected error: {ex.Message}"
                };
            }
        }
    }
}
