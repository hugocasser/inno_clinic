using System.Text.Json.Serialization;
using Grpc.Core;

namespace Application.Options;

public class GoogleApiClientOptions
{
    [JsonIgnore]
    public int ExpirationInSeconds { get; set; }
    public int MaxAttempts { get; set; } = 3;
    public int BackoffInSeconds { get; set; } = 5;
    public IEnumerable<StatusCode> FilterForStatusCodes { get; set; } = new List<StatusCode>()
    {
        StatusCode.Unavailable,
        StatusCode.Internal,
        StatusCode.Unknown,
        StatusCode.Aborted
    };

    public GoogleApiClientOptions()
    {
        ExpirationInSeconds = MaxAttempts * BackoffInSeconds;
    }
}