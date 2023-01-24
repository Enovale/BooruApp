using System.Collections.Generic;
using System.Threading.Tasks;
using BooruApp.Api.Models;

namespace BooruApp.Api.Interfaces
{
    public interface IServerProvider
    {
        public Task<bool> CompatibilityCheck();
        public Task<List<Post>?> SearchPosts(int page = 1, params string[] tags);
        public Task<List<Post>?> SearchPosts(params string[] tags);
    }
}