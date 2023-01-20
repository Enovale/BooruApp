using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using BooruApp.Api;
using BooruApp.Backend;
using BooruApp.DefaultProviders.Providers.DataFormat;

namespace BooruApp.DefaultProviders.Providers
{
    public class R34 : IProvider
    {
        public string DisplayName => "Rule 34";
        public string IdName => "r34xxx";

        private readonly string _baseUrl = "/index.php?page=dapi&q=index";

        // TODO: Clear duped code
        public async Task<List<Post>> SearchPosts(CancellationToken token, IServerConfig config, params string[] tags)
        {
            var url = tags.Length > 0
                ? GetSearchUrlWithParams(config.ApiUrl, "post", ("tags", string.Join(',', tags)))
                : GetSearchUrlWithParams(config.ApiUrl, "post");

            using var client = new HttpClient();
            var content = await client.GetStreamAsync(url);

            var serializer = new XmlSerializer(typeof(List<R34Post>), new XmlRootAttribute("posts"));
            var doc = (List<R34Post>)serializer.Deserialize(content);

            return await GetPostsFromList(doc);
        }

        public async Task<List<Tag>> SearchTagsByNames(CancellationToken token, IServerConfig config, string name)
        {
            var url = GetSearchUrlWithParams(config.ApiUrl, "tag", ("name", name));

            using var client = new HttpClient();
            var content = await client.GetStreamAsync(url);

            var serializer = new XmlSerializer(typeof(List<R34Tag>), new XmlRootAttribute("tags"));
            var doc = (List<R34Tag>)serializer.Deserialize(content);

            return GetTagsFromList(doc);
        }

        public async Task<List<Tag>> SearchTagsByNames(CancellationToken token, IServerConfig config, params string[] tags)
        {
            var url = tags.Length > 0
                ? GetSearchUrlWithParams(config.ApiUrl, "tag", ("names", string.Join(' ', tags)))
                : GetSearchUrlWithParams(config.ApiUrl, "tag");

            using var client = new HttpClient();
            var content = await client.GetStreamAsync(url);

            var serializer = new XmlSerializer(typeof(List<R34Tag>), new XmlRootAttribute("tags"));
            var doc = (List<R34Tag>)serializer.Deserialize(content);

            return GetTagsFromList(doc);
        }

        private string GetSearchUrlWithParams(string host, string search, params (string key, string val)[] parameters)
        {
            return GetUrlWithParams(host, parameters.Concat(new[] { ("s", search) }).ToArray());
        }

        private string GetUrlWithParams(string host, params (string key, string val)[] parameters)
        {
            var uri = new UriBuilder($"https://{host}{_baseUrl}");
            var query = HttpUtility.ParseQueryString(uri.Query);
            foreach (var param in parameters)
            {
                query.Add(param.key, param.val);
            }

            uri.Query = query.ToString();

            return uri.ToString();
        }

        private async Task<List<Post>> GetPostsFromList(List<R34Post> list)
        {
            var final = new List<Post>();

            foreach (var child in list)
            {
                final.Add(await GetPostFromXml(child));
            }

            return final;
        }

        private async Task<Post> GetPostFromXml(R34Post xml)
        {
            // TODO: R34xxx does not support `names` correctly???
            //var tags = await SearchTagsByNames(xml.Tags.Trim(' '));
            var tags = new List<Tag>() { };
            return new Post(
                xml.Id,
                tags,
                xml.FileUrl,
                xml.PreviewUrl,
                xml.Source,
                tags.Any(t => t.Name == "video")
            );
        }

        private List<Tag> GetTagsFromList(List<R34Tag> list)
        {
            var final = new List<Tag>();

            foreach (var child in list)
            {
                final.Add(GetTagFromXml(child));
            }

            return final;
        }

        private Tag GetTagFromXml(R34Tag xml)
        {
            return new Tag(
                xml.Id,
                xml.Name,
                xml.Type switch
                {
                    "0" => TagType.General,
                    "1" => TagType.Author,
                    "2" => TagType.Deprecated,
                    "3" => TagType.Copyright,
                    "4" => TagType.Character,
                    "5" => TagType.Metadata,
                    _ => TagType.Other
                }
            );
        }
    }
}