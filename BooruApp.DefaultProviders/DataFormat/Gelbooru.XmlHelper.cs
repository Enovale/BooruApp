using System.Reflection;
using System.Xml.Serialization;

namespace BooruApp.DefaultProviders.DataFormat
{
    public static class XmlHelpers
    {
        public static XmlAttributeOverrides GetOverrides<T>()
        {
            var overrides = new XmlAttributeOverrides();

            var type = typeof(T);
            foreach (var memberInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (memberInfo.GetCustomAttribute(typeof(XmlElementAttribute)) is XmlElementAttribute attr)
                {
                    overrides.Add(type, memberInfo.Name, new() { XmlAttribute = new(attr.ElementName) });
                }
            }

            return overrides;
        }
    }
}