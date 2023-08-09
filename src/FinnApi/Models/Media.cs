using FinnApi.Tools;
using System.Xml;

namespace FinnApi.Models;

public class Media
{
    #region Properties

    public string Url { get; private set; }

    public string? Type { get; private set; }

    public string? Medium { get; private set; }

    public int? Height { get; private set; }

    public int? Width { get; private set; }

    public List<MediaCategory>? Categories { get; private set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="node">Node.</param>
    /// <exception cref="Exception">Throw if any parsing goes wrong.</exception>
    public Media(XmlNode node)
    {
        this.Url = XmlParser.GetNodeAttributeValue(node, "url") ??
            throw new Exception("Unable to get URL node content.");

        this.Type = XmlParser.GetNodeAttributeValue(node, "type");
        this.Medium = XmlParser.GetNodeAttributeValue(node, "medium");

        if (int.TryParse(XmlParser.GetNodeAttributeValue(node, "height") ?? string.Empty, out var height))
        {
            this.Height = height;
        }

        if (int.TryParse(XmlParser.GetNodeAttributeValue(node, "width") ?? string.Empty, out var width))
        {
            this.Width = width;
        }

        this.Categories = XmlParser.ParseChildNodesAs<MediaCategory>(node, "media:category");
    }

    #endregion
}