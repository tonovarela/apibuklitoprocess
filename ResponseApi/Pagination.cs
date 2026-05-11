using System;
using System.Text.Json.Serialization;

namespace apiBukLitoprocess.responseApi;

public class Pagination
{
    [JsonPropertyName("next")]
    public Uri? Next { get; set; }

    [JsonPropertyName("previous")]
    public Uri? Previous { get; set; }

    [JsonPropertyName("count")]
    public long Count { get; set; }

    [JsonPropertyName("page")]
    public long Page { get; set; }

    [JsonPropertyName("totalPages")]
    public long TotalPages { get; set; }

}
