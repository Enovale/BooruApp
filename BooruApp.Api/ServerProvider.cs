namespace BooruApp.Api
{
    public abstract class ServerProvider
    {
        public string HostUrl { get; private set; }

        internal void InternalInit(string hostUrl)
            => (HostUrl) = (hostUrl);

        public virtual bool CompatibilityCheck() => true;
    }
}