using System.Xml;

namespace FinnApi.Tools;

internal static class XmlParser
{
    /// <summary>
    /// Get child node by name.
    /// </summary>
    /// <param name="parent">Parent node.</param>
    /// <param name="name">Name of child node.</param>
    /// <returns></returns>
    public static XmlNode? GetChildNode(XmlNode parent, string name)
    {
        return parent.ChildNodes
            .Cast<XmlNode>()
            .FirstOrDefault(n => n.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    }

    /// <summary>
    /// Get child nodes by name.
    /// </summary>
    /// <param name="parent">Parent node.</param>
    /// <param name="name">Name of child nodes.</param>
    /// <returns>All found nodes.</returns>
    public static IEnumerable<XmlNode> GetChildNodes(XmlNode parent, string name)
    {
        foreach (XmlNode child in parent.ChildNodes)
        {
            if (child.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
            {
                yield return child;
            }
        }
    }

    /// <summary>
    /// Get inner-text of a child node.
    /// </summary>
    /// <param name="parent">Parent node.</param>
    /// <param name="name">Name of child node.</param>
    /// <returns>Inner text of child node.</returns>
    public static string? GetChildNodeInnerText(XmlNode parent, string name)
    {
        return GetChildNode(parent, name)?.InnerText?.Trim();
    }

    /// <summary>
    /// Get attribute inner-text.
    /// </summary>
    /// <param name="node">Node.</param>
    /// <param name="name">Name of attribute.</param>
    /// <returns></returns>
    public static string? GetNodeAttributeValue(XmlNode node, string name)
    {
        var attr = node.Attributes?
            .Cast<XmlAttribute>()
            .FirstOrDefault(n => n.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

        return attr?.InnerText.Trim();
    }

    /// <summary>
    /// Parse node as type.
    /// </summary>
    /// <typeparam name="T">Type to instanciate.</typeparam>
    /// <param name="parent">Parent node.</param>
    /// <param name="name">Name of child node.</param>
    /// <returns>Type.</returns>
    public static T? ParseChildNodeAs<T>(XmlNode parent, string name)
    {
        var node = GetChildNode(parent, name);

        if (node is null)
        {
            return default;
        }

        return (T?)Activator.CreateInstance(typeof(T), node);
    }

    /// <summary>
    /// Parse inner-text of child node as DateTimeOffset.
    /// </summary>
    /// <param name="parent">Parent node.</param>
    /// <param name="name">Name of child node.</param>
    /// <returns>Parsed DateTimeOffset.</returns>
    public static DateTimeOffset? ParseChildNodeAsDateTime(XmlNode parent, string name)
    {
        var text = GetChildNode(parent, name)?.InnerText.Trim();

        if (text is null)
        {
            return null;
        }

        if (!DateTimeOffset.TryParse(text, out var dto))
        {
            return null;
        }

        return dto;
    }

    /// <summary>
    /// Get all child nodes that match name and parse them.
    /// </summary>
    /// <typeparam name="T">Type to instanciate.</typeparam>
    /// <param name="parent">Parent node.</param>
    /// <param name="name">Name of child nodes.</param>
    /// <returns>List of types.</returns>
    public static List<T> ParseChildNodesAs<T>(XmlNode parent, string name)
    {
        var list = new List<T>();

        foreach (var node in GetChildNodes(parent, name))
        {
            var ins = (T?)Activator.CreateInstance(typeof(T), node);

            if (ins is not null)
            {
                list.Add(ins);
            }
        }

        return list;
    }

    /// <summary>
    /// Get all data field nodes.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public static Dictionary<string, object?>? ParseDataNodeRecursive(XmlNode? parent)
    {
        if (parent is null)
        {
            return null;
        }

        var dict = new Dictionary<string, object?>();

        var entities = new[]
        {
            "finn:field",
            "finn:value",
            "finn:price"
        };

        foreach (var entity in entities)
        {
            var children = GetChildNodes(parent, entity);

            foreach (var child in children)
            {
                var name = GetNodeAttributeValue(child, "name") ?? Guid.NewGuid().ToString();

                if (child.ChildNodes.Count > 0 &&
                    child.InnerXml.Contains('<') &&
                    child.InnerXml.Contains('>'))
                {
                    dict.Add(
                        name,
                        ParseDataNodeRecursive(child));
                }
                else
                {
                    dict.Add(
                        name,
                        GetNodeAttributeValue(child, "value") ?? child.InnerText.Trim());
                }
            }
        }

        return dict;
    }
}