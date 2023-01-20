using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BooruApp.Api;
using BooruApp.Backend;

namespace BooruApp.Services;

public class ServerService : IServerService
{
    private readonly IProviderService _providerService;
    private readonly IAppStorageService _appStorageService;

    private BooruConfig _config => _appStorageService.Config;
    
    public IProvider? Provider => ServerConfig is not null ? _providerService.GetProvider(ServerConfig.Provider) : null;
    public ServerConfig? ServerConfig => _config.SelectedServer;
    
    public ServerService(IProviderService providerService, IAppStorageService appStorageService)
    {
        _providerService = providerService;
        _appStorageService = appStorageService;
    }

    public async Task<List<Post>> SearchPosts(CancellationToken token, params string[] tags)
    {
        if (Provider is null)
            return new();

        return await Provider.SearchPosts(token, ServerConfig, tags);
    }
    
    public async Task<List<Tag>> SearchTagsByNames(CancellationToken token, string name)
    {
        if (Provider is null)
            return new();

        return await Provider.SearchTagsByNames(token, ServerConfig, name);
    }
    
    public async Task<List<Tag>> SearchTagsByNames(CancellationToken token, params string[] tags)
    {
        if (Provider is null)
            return new();

        return await Provider.SearchTagsByNames(token, ServerConfig, tags);
    }
}