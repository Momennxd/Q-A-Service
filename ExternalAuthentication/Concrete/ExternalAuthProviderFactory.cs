using ExternalAuthentication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalAuthentication.Concrete
{
    public class ExternalAuthProviderFactory : IExternalAuthProviderFactory
    {
        private readonly IEnumerable<IExternalAuthProvider> _providers;

        public ExternalAuthProviderFactory(IEnumerable<IExternalAuthProvider> providers)
        {
            _providers = providers;
        }

        public IExternalAuthProvider GetProvider(string providerName)
        {
            return _providers.FirstOrDefault(p =>
                string.Equals(p.ProviderName, providerName, StringComparison.OrdinalIgnoreCase))
                ?? throw new ArgumentException("Unsupported provider");
        }
    }
}
