using System.Xml.Serialization;
#pragma warning disable CS8618

namespace BooruApp.DefaultProviders.Providers.DataFormat
{
    [XmlType("post")]
    public class R34Post
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        
        [XmlAttribute("created_at")]
        public string CreationDate { get; set; }
        
        [XmlAttribute("score")]
        public int Score { get; set; }
        
        [XmlAttribute("width")]
        public int Width { get; set; }
        
        [XmlAttribute("height")]
        public int Height { get; set; }
        
        [XmlAttribute("md5")]
        public string Md5 { get; set; }
        
        [XmlAttribute("directory")]
        public string Directory { get; set; }
        
        [XmlAttribute("image")]
        public string Image { get; set; }
        
        [XmlAttribute("rating")]
        public string Rating { get; set; }
        
        [XmlAttribute("source")]
        public string Source { get; set; }
        
        [XmlAttribute("change")]
        public int ChangeDate { get; set; }
        
        [XmlAttribute("owner")]
        public string Owner { get; set; }
        
        [XmlAttribute("creator_id")]
        public string CreatorId { get; set; }
        
        [XmlAttribute("parent_id")]
        public string ParentId { get; set; }
        
        [XmlAttribute("sample")]
        public int Sample { get; set; }
        
        [XmlAttribute("preview_width")]
        public int PreviewWidth { get; set; }
        
        [XmlAttribute("preview_height")]
        public int PreviewHeight { get; set; }

        [XmlAttribute("tags")]
        public string Tags { get; set; }

        [XmlAttribute("has_notes")]
        public bool HasNotes { get; set; }

        [XmlAttribute("has_comments")]
        public bool HasComments { get; set; }

        [XmlAttribute("file_url")]
        public string FileUrl { get; set; }
        
        [XmlAttribute("preview_url")]
        public string PreviewUrl { get; set; }
        
        [XmlAttribute("sample_url")]
        public string SampleUrl { get; set; }
        
        [XmlAttribute("sample_width")]
        public int SampleWidth { get; set; }
        
        [XmlAttribute("sample_height")]
        public int SampleHeight { get; set; }

        [XmlAttribute("status")]
        public string Status { get; set; }

        [XmlAttribute("post_locked")]
        public int PostLocked { get; set; }

        [XmlAttribute("has_children")]
        public bool HasChildren { get; set; }
    }

    [XmlType("tag")]
    public class R34Tag
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        
        [XmlAttribute("name")]
        public string Name { get; set; }
        
        [XmlAttribute("count")]
        public int Count { get; set; }
        
        [XmlAttribute("type")]
        public string Type { get; set; }
        
        [XmlAttribute("ambiguous")]
        public int Ambiguous { get; set; }
    }
}