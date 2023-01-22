namespace BooruApp.Api.Interfaces
{
    public interface IServerProviderMetadata
    {
        public string DisplayName { get; }
        public string IdName { get; }
        public string? DefaultHostUri { get; }
    }
}