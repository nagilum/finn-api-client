using FinnApi.Tools;
using System.Globalization;
using System.Xml;

namespace FinnApi.Models;

public class Geolocation
{
    #region Properties

    public double Latitude { get; private set; }

    public double Longitude { get; private set; }

    public double? Accuracy { get; private set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="node">Node.</param>
    /// <exception cref="Exception">Throw if any parsing goes wrong.</exception>
    public Geolocation(XmlNode node)
    {
        var text = node.InnerText.Trim();
        var points = text.Split(' ');

        if (points.Length != 2)
        {
            throw new Exception("Node inner-text does not contain a valid geo location.");
        }

        var ci = new CultureInfo("en-US");

        if (!double.TryParse(points[0], NumberStyles.Any, ci, out var lat) ||
            !double.TryParse(points[1], NumberStyles.Any, ci, out var lng))
        {
            throw new Exception("Node inner-text does not contain a valid geo location.");
        }

        this.Latitude = lat;
        this.Longitude = lng;

        var accuracy = XmlParser.GetNodeAttributeValue(node, "accuracy") ??
                       XmlParser.GetNodeAttributeValue(node, "finn:accuracy");

        if (double.TryParse(accuracy, out var value))
        {
            this.Accuracy = value;
        }
    }

    #endregion
}