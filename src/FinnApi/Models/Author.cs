using FinnApi.Tools;
using System.Xml;

namespace FinnApi.Models;

public class Author
{
    #region Properties

    public string? Id { get; private set; }

    public string? ExternalRef { get; private set; }

    public string? Name { get; private set; }

    public string? Uri { get; private set; }

    public List<Link>? Links { get; private set; }

    public List<Media>? Media { get; private set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="node">Node.</param>
    /// <exception cref="Exception">Throw if any parsing goes wrong.</exception>
    public Author(XmlNode node)
    {
        this.Id = XmlParser.GetChildNodeInnerText(node, "id") ??
                  XmlParser.GetChildNodeInnerText(node, "dc:identifier");

        if (this.Id?.Contains(':') == true)
        {
            this.Id = this.Id[(this.Id.LastIndexOf(':') + 1)..];
        }

        this.ExternalRef = XmlParser.GetChildNodeInnerText(node, "externalref") ??
                           XmlParser.GetChildNodeInnerText(node, "finn:externalref");

        this.Name = XmlParser.GetChildNodeInnerText(node, "name");
        this.Uri = XmlParser.GetChildNodeInnerText(node, "uri");

        this.Links = XmlParser.ParseChildNodesAs<Link>(node, "link");
        this.Media = XmlParser.ParseChildNodesAs<Media>(node, "media:content");
    }

    #endregion
}