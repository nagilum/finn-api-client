# Finn API Client

FINN.no is Norway's biggest online marketplace.
They have an API, which you can read more about at [finn.no/api](https://www.finn.no/api/).
This client provides a wrapper around that API and returns easy to use classes for reading their data.
You will need an API key, and also an organization ID to access specific data tied to said organizations.

I wrote the wrapper to access job ads from their system, but it should be able to access the rest of the resources under the `ResourceType` enum as well.

## Create an Instance of the API Client

```csharp
const string apiKey = "your-api-key";
const string orgId = "your-finn-org-id";

var api = new FinnApiClient(apiKey, orgId);
```

Or without the orgId:

```csharp
const string apiKey = "your-api-key";

var api = new FinnApiClient(apiKey);
```

## Get All Entries of a Type

```csharp
var entries = await api.GetEntriesAsync(ResourceType.JobFullTime);
```

This will give you a list of all entries from the type `job-full-time`.

The API supports a bunch of parameters for filtering, which I didn't need for my purposed, but I included a dictionary for that. That goes in as the second parameter, while the third is a cancellation token if you want to provide that. Both the filter and cancellation token are optional.

### Example

```csharp
var filter = new Dictionary<string, string>
{
    {"location", "0.5"}
};

var entries = await api.GetEntriesAsync(
    ResourceType.JobFullTime,
    filter,
    myCancellationToken);
```

If you want to access multiple organizations with the same client, you can forgo adding in the orgId when you instanciate the API client and add it as a filter.

```csharp
const string apiKey = "your-api-key";

const string orgId1 = "org-id-1";
const string orgId2 = "org-id-2";

var api = new FinnApiClient(apiKey);

var entries_org_1 = await api.GetEntriesAsync(
    ResourceType.JobFullTime,
    new()
    {
        {"orgId", orgId1}
    });

var entries_org_2 = await api.GetEntriesAsync(
    ResourceType.JobFullTime,
    new()
    {
        {"orgId", orgId2}
    });
```

## Get a Single Finn Ad

```csharp
var entry = await api.GetEntryAsync(ResourceType.JobFullTime, "single-entry-finn-id");
```

This function also has a third optional parameter for cancellation token.

```csharp
var entry = await api.GetEntryAsync(
    ResourceType.JobFullTime,
    "single-entry-finn-id",
    myCancellationToken);
```