using FinnApi.Tools;
using System.Xml;

namespace FinnApi.Models;

public class MediaCategory
{
    #region Properties

    public string Name { get; private set; }

    public string? Scheme { get; private set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="node">Node.</param>
    public MediaCategory(XmlNode node)
    {
        this.Name = node.InnerText.Trim();
        this.Scheme = XmlParser.GetNodeAttributeValue(node, "scheme");
    }

    #endregion
}