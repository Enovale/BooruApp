using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BooruApp.Api.Interfaces;
using BooruApp.Api.Models;

namespace BooruApp.Api
{
    public abstract class ServerProvider : IServerProvider
    {
        public string HostUrl { get; private set; }

        internal void InternalInit(string hostUrl)
            => (HostUrl) = (hostUrl);

        public virtual Task<bool> CompatibilityCheck() => Task.FromResult(true);

        public abstract Task<List<Post>> SearchPosts(params string[] tags);
    }
}