using FinnApi.Tools;
using System.Xml;

namespace FinnApi.Models;

public class Entry
{
    #region Properties

    public string Id { get; private set; }

    public string Type { get; private set; }

    public string? Title { get; private set; }

    public DateTimeOffset? Updated { get; private set; }

    public DateTimeOffset? Published { get; private set; }

    public DateTimeOffset? Expires { get; private set; }

    public DateTimeOffset? DateSubmitted { get; private set; }

    public DateTimeOffset? Edited { get; private set; }

    public Author? Author { get; private set; }

    public Location? Location { get; private set; }

    public Geolocation? Point { get; private set; }

    public List<Category>? Categories { get; private set; }

    public List<Contact> Contacts { get; private set; }

    public List<Link>? Links { get; private set; }

    public List<Media>? Media { get; private set; }

    public Dictionary<string, object?>? Data { get; private set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="node">Entry node.</param>
    /// <param name="type">Type of resource.</param>
    /// <exception cref="Exception">Throw if any parsing goes wrong.</exception>
    public Entry(XmlNode node, string type)
    {
        this.Type = type;
        this.Id = XmlParser.GetChildNodeInnerText(node, "id") ??
                  XmlParser.GetChildNodeInnerText(node, "dc:identifier") ??
                  throw new Exception("XML node does not contain an ID child node.");

        if (this.Id.Contains(':'))
        {
            this.Id = this.Id[(this.Id.LastIndexOf(':') + 1)..];
        }

        this.Title = XmlParser.GetChildNodeInnerText(node, "title");

        this.Updated = XmlParser.ParseChildNodeAsDateTime(node, "updated");
        this.Published = XmlParser.ParseChildNodeAsDateTime(node, "published");
        this.Expires = XmlParser.ParseChildNodeAsDateTime(node, "expires") ??
                       XmlParser.ParseChildNodeAsDateTime(node, "age:expires");
        this.DateSubmitted = XmlParser.ParseChildNodeAsDateTime(node, "dateSubmitted") ??
                             XmlParser.ParseChildNodeAsDateTime(node, "dc:dateSubmitted");
        this.Edited = XmlParser.ParseChildNodeAsDateTime(node, "edited") ??
                      XmlParser.ParseChildNodeAsDateTime(node, "app:edited");

        this.Author = XmlParser.ParseChildNodeAs<Author>(node, "author") ??
                      XmlParser.ParseChildNodeAs<Author>(node, "finn:author");

        this.Location = XmlParser.ParseChildNodeAs<Location>(node, "location") ??
                        XmlParser.ParseChildNodeAs<Location>(node, "finn:location");

        this.Point = XmlParser.ParseChildNodeAs<Geolocation>(node, "geo:point") ??
                     XmlParser.ParseChildNodeAs<Geolocation>(node, "georss:point");

        this.Categories = XmlParser.ParseChildNodesAs<Category>(node, "category");
        this.Contacts = XmlParser.ParseChildNodesAs<Contact>(node, "finn:contact");
        this.Links = XmlParser.ParseChildNodesAs<Link>(node, "link");
        this.Media = XmlParser.ParseChildNodesAs<Media>(node, "media:content");

        this.Data = XmlParser.ParseDataNodeRecursive(
            XmlParser.GetChildNode(node, "finn:adata"));
    }

    #endregion
}