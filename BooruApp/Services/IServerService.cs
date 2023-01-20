using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BooruApp.Api;
using BooruApp.Backend;
using Glitonea.Mvvm;

namespace BooruApp.Services;

public interface IServerService : IService
{
    public IProvider? Provider { get; }
    public ServerConfig? ServerConfig { get; }
    
    public Task<List<Post>> SearchPosts(CancellationToken token, params string[] tags);
    public Task<List<Tag>> SearchTagsByNames(CancellationToken token, string name);
    public Task<List<Tag>> SearchTagsByNames(CancellationToken token, params string[] tags);
}