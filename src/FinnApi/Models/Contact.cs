using FinnApi.Tools;
using System.Xml;

namespace FinnApi.Models;

public class Contact
{
    #region Properties

    public string? Email { get; private set; }

    public string? Name { get; private set; }

    public string? Role { get; private set; }

    public string? Title { get; private set; }

    public List<Link>? Links { get; private set; }

    public List<PhoneNumber>? PhoneNumbers { get; private set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="node">Contact node.</param>
    public Contact(XmlNode node)
    {
        this.Email = XmlParser.GetChildNodeInnerText(node, "email");
        this.Name = XmlParser.GetChildNodeInnerText(node, "name");
        this.Role = XmlParser.GetNodeAttributeValue(node, "role");
        this.Title = XmlParser.GetNodeAttributeValue(node, "title");

        this.Links = XmlParser.ParseChildNodesAs<Link>(node, "link");
        this.PhoneNumbers = XmlParser.ParseChildNodesAs<PhoneNumber>(node, "finn:phone-number");
    }

    #endregion
}