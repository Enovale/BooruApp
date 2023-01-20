using System;

namespace BooruApp.Backend;

[Serializable]
public class Tag
{
    public string Id { get; }
    public string Name { get; }
    public TagType Type { get; }

    public Tag(string id, string name, TagType type)
    {
        Id = id;
        Name = name;
        Type = type;
    }
}