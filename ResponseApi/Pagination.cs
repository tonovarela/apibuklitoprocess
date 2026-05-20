using System.Text.Json.Serialization;

namespace apiBukLitoprocess.responseApi;

public class Pagination
{
    // [JsonPropertyName("next")]
    // public Uri? Next { get; set; }

    // [JsonPropertyName("previous")]
    // public Uri? Previous { get; set; }

    // [JsonPropertyName("count")]
    // public int Count { get; set; }


      [JsonPropertyName("totalPages")]
    public int totalPages { get; set; }
    
    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

}
