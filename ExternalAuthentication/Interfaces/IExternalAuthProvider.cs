using ExternalAuthentication.DateToSend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalAuthentication.Interfaces
{
    public interface IExternalAuthProvider
    {
        string ProviderName { get; }
        Task<ExternalAuthResults> AuthenticateGoogleAsync(string idToken);

    }
}
