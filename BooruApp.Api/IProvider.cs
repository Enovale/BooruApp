using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BooruApp.Backend;

namespace BooruApp.Api
{
    public interface IProvider
    {
        public string DisplayName { get; }
        public string IdName { get; }
        public Task<List<Post>> SearchPosts(CancellationToken token, IServerConfig config, params string[] tags);
        public Task<List<Tag>> SearchTagsByNames(CancellationToken token, IServerConfig config, string name);
        public Task<List<Tag>> SearchTagsByNames(CancellationToken token, IServerConfig config, params string[] tags);
    }
}