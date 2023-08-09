namespace FinnApi;

[AttributeUsage(AttributeTargets.All)]
public class FinnPartialUrlAttribute : Attribute
{
    /// <summary>
    /// Partial finn.no URL segment.
    /// </summary>
    protected string FinnPartialUrlValue { get; set; }

    /// <summary>
    /// Get the string stored in this attribute.
    /// </summary>
    public virtual string PartialUrl => this.FinnPartialUrlValue;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="partialUrl">Partial finn.no URL segment.</param>
    public FinnPartialUrlAttribute(string partialUrl)
    {
        this.FinnPartialUrlValue = partialUrl;
    }
}