using BooruApp.Api;
using BooruApp.Api.Interfaces;

namespace BooruApp.Infrastructure.Models
{
    public class RegisteredServerProvider
    {
        public ServerProvider ProviderInstance { get; }
        public IServerProviderMetadata Metadata { get; }

        public RegisteredServerProvider(ServerProvider instance, IServerProviderMetadata metadata)
            => (ProviderInstance, Metadata) = (instance, metadata);
    }
}