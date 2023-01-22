using BooruApp.Api.Interfaces;

namespace BooruApp.Api
{
    public class ServerProviderAttribute : Attribute, IServerProviderMetadata
    {
        public string DisplayName { get; set; }
        public string IdName { get; set; }
        public string? DefaultHostUri { get; set; }

        public ServerProviderAttribute(string displayName, string idName, string? defaultHostUri = null)
            => (DisplayName, IdName, DefaultHostUri) = (displayName, idName, defaultHostUri);
    }
}