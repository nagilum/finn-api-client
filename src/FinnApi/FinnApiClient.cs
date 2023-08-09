using FinnApi.Models;
using FinnApi.Tools;
using System.Xml;

namespace FinnApi;

public class FinnApiClient
{
    #region Properties

    /// <summary>
    /// Base API URL.
    /// </summary>
    private const string BaseUrl = "https://cache.api.finn.no/iad/";

    /// <summary>
    /// HTTP client.
    /// </summary>
    private readonly HttpClient HttpClient = new();

    /// <summary>
    /// Finn organization number.
    /// </summary>
    private string? OrgId { get; set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="apiKey">API key.</param>
    /// <param name="orgId">Finn organization number.</param>
    public FinnApiClient(string apiKey, string? orgId = null)
    {
        this.OrgId = orgId;

        this.HttpClient.DefaultRequestHeaders.Add("Accept", "*/*");
        this.HttpClient.DefaultRequestHeaders.Add("x-FINN-apikey", apiKey);
    }

    #endregion

    #region API functions

    /// <summary>
    /// Get a single resource entry from the Finn API.
    /// </summary>
    /// <param name="resourceType">Type of resource.</param>
    /// <param name="id">Finn entry ID.</param>
    /// <param name="ctoken">Cancellation token.</param>
    /// <returns>Entry.</returns>
    /// <exception cref="Exception">Throw if an unhandled exception occurrs.</exception>
    /// <exception cref="XmlException">Throw if XML document failed to load.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if the entry root node was not found.</exception>
    /// <exception cref="HttpRequestException">Thrown if the request fatally failed.</exception>
    public async Task<Entry> GetEntryAsync(
        ResourceType resourceType,
        string id,
        CancellationToken ctoken = default)
    {
        var type = GetResourceTypePartialUrl(resourceType);
        var url = $"{BaseUrl}ad/{type}/{id}";
        var xml = await this.HttpClient.GetStringAsync(url, ctoken);

        var doc = new XmlDocument();
        doc.LoadXml(xml);

        var entry = doc.ChildNodes
            .Cast<XmlNode>()
            .FirstOrDefault(n => n.Name == "entry") ??
            throw new KeyNotFoundException(
                "Entry root node not found in XML document.");

        return new(entry, type);
    }

    /// <summary>
    /// Get resource entries from the Finn API.
    /// </summary>
    /// <param name="resourceType">Type of resource.</param>
    /// <param name="options">Filtering options.</param>
    /// <param name="ctoken">Cancellation token.</param>
    /// <returns>List of entries.</returns>
    /// <exception cref="Exception">Throw if an unhandled exception occurrs.</exception>
    /// <exception cref="XmlException">Throw if XML document failed to load.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if the feed root node was not found.</exception>
    /// <exception cref="HttpRequestException">Thrown if the request fatally failed.</exception>
    public async Task<List<Entry>> GetEntriesAsync(
        ResourceType resourceType,
        Dictionary<string, string?>? options = null,
        CancellationToken ctoken = default)
    {
        var type = GetResourceTypePartialUrl(resourceType);
        var url = $"{BaseUrl}search/{type}{CompileQueryStringParams(this.OrgId, options)}";
        var xml = await this.HttpClient.GetStringAsync(url, ctoken);

        var doc = new XmlDocument();
        doc.LoadXml(xml);

        var feed = doc.ChildNodes
            .Cast<XmlNode>()
            .FirstOrDefault(n => n.Name == "feed") ??
            throw new KeyNotFoundException(
                "Feed root node not found in XML document.");

        return XmlParser.GetChildNodes(feed, "entry")
            .Select(n => new Entry(n, type))
            .ToList();
    }

    #endregion

    #region Helper functions

    /// <summary>
    /// Compile query-string params.
    /// </summary>
    /// <param name="orgId">Finn organization number.</param>
    /// <param name="options">Filtering options.</param>
    /// <returns></returns>
    private static string CompileQueryStringParams(
        string? orgId = null,
        Dictionary<string, string?>? options = null)
    {
        options ??= new();

        if (orgId is not null)
        {
            options.Add("orgId", orgId);
        }

        return options.Count > 0
            ? $"?{string.Join("&", options.Select(n => $"{n.Key}={n.Value}"))}"
            : string.Empty;
    }

    /// <summary>
    /// Get partial URL from enum value.
    /// </summary>
    /// <param name="resourceType">Enum value.</param>
    /// <returns>Partial URL.</returns>
    private static string GetResourceTypePartialUrl(
        ResourceType resourceType)
    {
        var fieldInfo = resourceType.GetType().GetField(resourceType.ToString());

        if (fieldInfo is null)
        {
            return resourceType.ToString();
        }

        var attributes =
            (FinnPartialUrlAttribute[])fieldInfo.GetCustomAttributes(
                typeof(FinnPartialUrlAttribute),
                false);

        return attributes?.Length > 0
            ? attributes[0].PartialUrl
            : resourceType.ToString();
    }

    #endregion
}