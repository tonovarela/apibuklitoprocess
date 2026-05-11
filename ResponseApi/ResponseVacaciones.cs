namespace apiBukLitoprocess.responseApi;

using System.Text.Json.Serialization;

public class ResponseVacaciones
{
    [JsonPropertyName("pagination")]
    public required PaginationVacaciones pagination { get; set; }

    [JsonPropertyName("data")]
    public required List<VacacionesRest> Data { get; set; }
}

public partial class VacacionesRest
{
    [JsonPropertyName("id")]
    public long id { get; set; }

    [JsonPropertyName("employee_id")]
    public long employee_id { get; set; }

    [JsonPropertyName("approved_by_id")]
    public long approved_by_id { get; set; }

    [JsonPropertyName("approved_at")]
    public DateTimeOffset approved_at { get; set; }

    [JsonPropertyName("working_days")]
    public long working_days { get; set; }

    [JsonPropertyName("calendar_days")]
    public long calendar_days { get; set; }

    [JsonPropertyName("workday_stage")]
    public required string workday_stage { get; set; }

    [JsonPropertyName("start_date")]
    public DateOnly start_date { get; set; }

    [JsonPropertyName("end_date")]
    public DateOnly end_date { get; set; }

    [JsonPropertyName("type")]
    public required string type { get; set; }

    [JsonPropertyName("requested_at")]
    public DateTimeOffset requested_at { get; set; }

    [JsonPropertyName("status")]
    public required string status { get; set; }

    [JsonPropertyName("vacation_type_id")]
    public long vacation_type_id { get; set; }
}

public partial class PaginationVacaciones
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