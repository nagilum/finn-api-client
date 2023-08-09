using FinnApi.Tools;
using System.Xml;

namespace FinnApi.Models;

public class Category
{
    #region Properties

    public string? Scheme { get; private set; }

    public string? Label { get; private set; }

    public string? Term { get; private set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="node">Category node.</param>
    public Category(XmlNode node)
    {
        this.Scheme = XmlParser.GetNodeAttributeValue(node, "scheme");
        this.Label = XmlParser.GetNodeAttributeValue(node, "label");
        this.Term = XmlParser.GetNodeAttributeValue(node, "term");
    }

    #endregion
}