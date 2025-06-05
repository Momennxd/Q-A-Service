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
        private readonly Dictionary<string, IExternalAuthProvider> _providers;

        public ExternalAuthProviderFactory(IEnumerable<IExternalAuthProvider> providers)
        {
            _providers = providers.ToDictionary(
                p => p.ProviderName,
                StringComparer.OrdinalIgnoreCase);
        }

        public IExternalAuthProvider GetProvider(string providerName)
        {
            return _providers.TryGetValue(providerName, out var provider)
                ? provider
                : throw new ArgumentException("Unsupported provider");
        }
    }

}
