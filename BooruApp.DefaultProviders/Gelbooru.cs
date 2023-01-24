using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using BooruApp.Api;
using BooruApp.Api.Models;
using BooruApp.DefaultProviders.DataFormat;

namespace BooruApp.DefaultProviders
{
    [ServerProvider("Gelbooru", "gelbooru", "gelbooru.com")]
    public class Gelbooru : ServerProvider
    {
        private const string _baseUrl = "/index.php?page=dapi&q=index";

        public override async Task<bool> CompatibilityCheck()
        {
            var result = await Search<Post>(GetSearchUrlWithParams("post", ("limit", "1")), "posts");
            
            if (result is null)
            {
                
                return false;
            }

            return true;
        }

        private string GetSearchUrlWithParams(string search, params (string key, string val)[] parameters)
        {
            return GetUrlWithParams(parameters.Concat(new[] { ("s", search) }).ToArray());
        }

        private string GetUrlWithParams(params (string key, string val)[] parameters)
        {
            var uri = new UriBuilder($"https://{HostUrl}{_baseUrl}");
            var query = HttpUtility.ParseQueryString(uri.Query);
            foreach (var param in parameters)
            {
                query.Add(param.key, param.val);
            }

            uri.Query = query.ToString();

            return uri.ToString();
        }
        
        public override async Task<List<Post>?> SearchPosts(int page = 0, params string[] tags)
        {
            var url = GetSearchUrlWithParams("post", ("pid", page.ToString()), ("tags", string.Join(',', tags)));
            Console.WriteLine(url);

            var res = await Search<GelbooruPost>(url, "posts");
            
            return res.Select(p => new Post(p.FileUrl, p.PreviewUrl)).ToList();
        }

        public override Task<List<Post>?> SearchPosts(params string[] tags)
            => SearchPosts(0, tags);

        private async Task<List<T>> Search<T>(string url, string rootName)
        {
            using var client = new HttpClient();
            var content = await client.GetStreamAsync(url);

            var serializer = new XmlSerializer(typeof(List<T>), new XmlRootAttribute(rootName));
            return (List<T>)serializer.Deserialize(content);
        }
    }
}