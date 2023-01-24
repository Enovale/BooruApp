using System.Collections.Generic;
using System.Threading.Tasks;
using BooruApp.Api;
using BooruApp.Api.Models;

namespace BooruApp.DefaultProviders
{
    [ServerProvider("Test", "test")]
    public class Test : ServerProvider
    {
        public override Task<List<Post>?> SearchPosts(int page = 0, params string[] tags)
        {
            throw new System.NotImplementedException();
        }

        public override Task<List<Post>?> SearchPosts(params string[] tags)
        {
            throw new System.NotImplementedException();
        }
    }
}