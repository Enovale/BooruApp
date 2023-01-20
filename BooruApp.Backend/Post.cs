using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BooruApp.Backend
{
    [Serializable]
    public class Post
    {
        [JsonInclude]
        public string Id { get; }

        [JsonInclude]
        [JsonPropertyName("Tags")]
        private List<Tag> _tags { get; }

        [JsonIgnore]
        public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();
        
        [JsonInclude]
        public string? ImageUrl { get; }
        
        [JsonInclude]
        public string? PreviewUrl { get; }
        
        [JsonInclude]
        public string? Source { get; }
        
        [JsonInclude]
        public bool Video { get; } = false;

        [JsonConstructor]
        public Post(string id, List<Tag>? tags, string? imageUrl, string? previewUrl, string? source, bool video)
            => (Id, _tags, ImageUrl, PreviewUrl, Source, Video) = (id, tags ?? new(), imageUrl, previewUrl, source, video);
    }
}