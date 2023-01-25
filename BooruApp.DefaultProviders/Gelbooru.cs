using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
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
        private static readonly HttpClient _client = new();

        private const string _baseUrl = "/index.php?page=dapi&q=index";

        static Gelbooru()
        {
            var name = Assembly.GetExecutingAssembly().GetName();
            _client.DefaultRequestHeaders.UserAgent.Clear();
            _client.DefaultRequestHeaders.UserAgent.ParseAdd(name.Name + "/" + name.Version);
        }

        public override async Task<bool> CompatibilityCheck()
        {
            var result = await Search<GelbooruPost>(GetSearchUrlWithParams("post", ("limit", "1")), "posts");

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

            var res = await Search<GelbooruPost>(url, "posts");

            return res?.Select(p => new Post(p.FileUrl, p.PreviewUrl)).ToList();
        }

        public override Task<List<Post>?> SearchPosts(params string[] tags)
            => SearchPosts(0, tags);

        private async Task<List<T>?> Search<T>(string url, string rootName)
        {
            Stream content;
            try
            {
                content = await _client.GetStreamAsync(url);
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode is null)
                    Trace.TraceError("Unknown HTTP request error: {0}", e);
                else
                    Trace.TraceError("HTTP request returned error code: {0} {1}", (int)e.StatusCode, e.StatusCode);

                return null;
            }

            // Needed to support old 0.2.0 beta API
            // TODO: Implement configuration API and make this a toggle :)
            var overrides = XmlHelpers.GetOverrides<T>();
            var serializer = new XmlSerializer(typeof(List<T>), overrides, null, new XmlRootAttribute(rootName), null);

            List<T>? res = null;
            try
            {
                res = (List<T>?)serializer.Deserialize(content);
            }
            catch (Exception e)
            {
                Trace.TraceError("Malformed XML exception: {0}", e);
            }

#if DEBUG
            if (res is null)
            {
                var xml = await _client.GetStringAsync(url);
                await File.WriteAllTextAsync("crashed.xml", xml);
                Trace.TraceError(xml);
            }
#endif

            return res;
        }
    }
}