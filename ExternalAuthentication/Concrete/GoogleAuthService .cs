using ExternalAuthentication.DateToSend;
using ExternalAuthentication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalAuthentication.Concrete
{
    public class GoogleAuthService: IExternalAuthProvider
    {
        public string ProviderName => "Google";
        public async Task<ExternalAuthResults> AuthenticateGoogleAsync(string idToken)
        {
            
        }
    }
    
    
}
