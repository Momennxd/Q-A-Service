using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalAuthentication.Interfaces
{
    public interface IExternalAuthProviderFactory
    {
        IExternalAuthProvider GetProvider(string providerName);
    }
}
