using FinnApi.Tools;
using System.Xml;

namespace FinnApi.Models;

public class Location
{
    #region Properties

    public string? Address { get; private set; }

    public string? City { get; private set; }

    public string? Country { get; private set; }

    public string? PostalCode { get; private set; }

    public List<Link>? Links { get; private set; }

    public List<Media>? Media { get; private set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="node">Node.</param>
    public Location(XmlNode node)
    {
        this.Address = XmlParser.GetChildNodeInnerText(node, "address") ??
                       XmlParser.GetChildNodeInnerText(node, "finn:address");

        this.City = XmlParser.GetChildNodeInnerText(node, "city") ??
                    XmlParser.GetChildNodeInnerText(node, "finn:city");

        this.Country = XmlParser.GetChildNodeInnerText(node, "country") ??
                       XmlParser.GetChildNodeInnerText(node, "finn:country");

        this.PostalCode = XmlParser.GetChildNodeInnerText(node, "postal-code") ??
                          XmlParser.GetChildNodeInnerText(node, "finn:postal-code");

        this.Links = XmlParser.ParseChildNodesAs<Link>(node, "link");
        this.Media = XmlParser.ParseChildNodesAs<Media>(node, "media:content");
    }

    #endregion
}