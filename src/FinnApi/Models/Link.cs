using FinnApi.Tools;
using System.Xml;

namespace FinnApi.Models;

public class Link
{
    #region Properties

    public string Href { get; private set; }

    public string? Rel { get; private set; }

    public string? Type { get; private set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="node">Link node.</param>
    /// <exception cref="Exception">Throw if any parsing goes wrong.</exception>
    public Link(XmlNode node)
    {
        this.Href = XmlParser.GetNodeAttributeValue(node, "href") ??
            throw new Exception("Unable to get HREF attribute from node.");

        this.Rel = XmlParser.GetNodeAttributeValue(node, "rel");
        this.Type = XmlParser.GetNodeAttributeValue(node, "type");
    }

    #endregion
}