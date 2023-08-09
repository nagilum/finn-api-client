using FinnApi.Tools;
using System.Xml;

namespace FinnApi.Models;

public class PhoneNumber
{
    #region Properties

    public string? Number { get; private set; }

    public string? Type { get; private set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="node">Phone number node.</param>
    public PhoneNumber(XmlNode node)
    {
        this.Number = node.InnerText.Trim();
        this.Type = XmlParser.GetNodeAttributeValue(node, "type");
    }

    #endregion
}