using System.Xml.Serialization;

namespace BooruApp.DefaultProviders.DataFormat
{
    [XmlType("post")]
    public class GelbooruPost
    {
        [XmlElement("id")]
        public string Id { get; set; }
        
        [XmlElement("created_at")]
        public string CreationDate { get; set; }
        
        [XmlElement("score")]
        public int Score { get; set; }
        
        [XmlElement("width")]
        public int Width { get; set; }
        
        [XmlElement("height")]
        public int Height { get; set; }
        
        [XmlElement("md5")]
        public string Md5 { get; set; }
        
        [XmlElement("directory")]
        public string Directory { get; set; }
        
        [XmlElement("image")]
        public string Image { get; set; }
        
        [XmlElement("rating")]
        public string Rating { get; set; }
        
        [XmlElement("source")]
        public string Source { get; set; }
        
        [XmlElement("change")]
        public int ChangeDate { get; set; }
        
        [XmlElement("owner")]
        public string Owner { get; set; }
        
        [XmlElement("creator_id")]
        public string CreatorId { get; set; }
        
        [XmlElement("parent_id")]
        public string ParentId { get; set; }
        
        [XmlElement("sample")]
        public int Sample { get; set; }
        
        [XmlElement("preview_width")]
        public int PreviewWidth { get; set; }
        
        [XmlElement("preview_height")]
        public int PreviewHeight { get; set; }

        [XmlElement("tags")]
        public string Tags { get; set; }

        [XmlElement("has_notes")]
        public bool HasNotes { get; set; }

        [XmlElement("has_comments")]
        public bool HasComments { get; set; }

        [XmlElement("file_url")]
        public string FileUrl { get; set; }
        
        [XmlElement("preview_url")]
        public string PreviewUrl { get; set; }
        
        [XmlElement("sample_url")]
        public string SampleUrl { get; set; }
        
        [XmlElement("sample_width")]
        public int SampleWidth { get; set; }
        
        [XmlElement("sample_height")]
        public int SampleHeight { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("post_locked")]
        public int PostLocked { get; set; }

        [XmlElement("has_children")]
        public bool HasChildren { get; set; }

        public static XmlAttributeOverrides R34Overrides()
        {
            // TODO
            var overrides = new XmlAttributeOverrides();

            overrides.Add(typeof(GelbooruPost), nameof(Id), new XmlAttributes() {XmlAttribute = new XmlAttributeAttribute(nameof(Id))});
            overrides.Add(typeof(GelbooruPost), nameof(FileUrl), new XmlAttributes() {XmlAttribute = new XmlAttributeAttribute(nameof(FileUrl))});
            overrides.Add(typeof(GelbooruPost), nameof(PreviewUrl), new XmlAttributes() {XmlAttribute = new XmlAttributeAttribute(nameof(PreviewUrl))});

            return overrides;
        }
    }

    [XmlType("tag")]
    public class GelbooruTag
    {
        [XmlElement("id")]
        public string Id { get; set; }
        
        [XmlElement("name")]
        public string Name { get; set; }
        
        [XmlElement("count")]
        public int Count { get; set; }
        
        [XmlElement("type")]
        public string Type { get; set; }
        
        [XmlElement("ambiguous")]
        public int Ambiguous { get; set; }
    }
}