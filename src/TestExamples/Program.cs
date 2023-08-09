using FinnApi;

namespace TestExamples;

internal static class Program
{
    private static async Task Main()
    {
        const string apiKey = "your-api-key";
        const string orgId = "your-finn-org-id";

        var api = new FinnApiClient(apiKey, orgId);

        var entries = await api.GetEntriesAsync(ResourceType.JobFullTime);
        var entry = await api.GetEntryAsync(ResourceType.JobFullTime, "single-entry-finn-id");
    }
}