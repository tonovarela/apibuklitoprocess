namespace apiBukLitoprocess.responseApi;

using System.Text.Json.Serialization;
using apiBukLitoprocess.helpers;

public class ResponseVacaciones
{
    [JsonPropertyName("pagination")]
    public required Pagination pagination { get; set; }

    [JsonPropertyName("data")]
    public required List<VacacionesRest> Data { get; set; }
}

public partial class VacacionesRest
{
    [JsonPropertyName("id")]
    [JsonConverter(typeof(FlexibleLongConverter))]
    public long id { get; set; }

    [JsonPropertyName("employee_id")]
    [JsonConverter(typeof(FlexibleLongConverter))]
    public long employee_id { get; set; }

    [JsonPropertyName("approved_by_id")]
    [JsonConverter(typeof(FlexibleNullableLongConverter))]
    public long? approved_by_id { get; set; }

    [JsonPropertyName("approved_at")]
    public DateTimeOffset? approved_at { get; set; }

    [JsonPropertyName("working_days")]
    [JsonConverter(typeof(FlexibleNullableLongConverter))]
    public long? working_days { get; set; }

    [JsonPropertyName("calendar_days")]
    [JsonConverter(typeof(FlexibleNullableLongConverter))]
    public long? calendar_days { get; set; }

    [JsonPropertyName("workday_stage")]
    public string? workday_stage { get; set; }

    [JsonPropertyName("start_date")]
    public DateOnly? start_date { get; set; }

    [JsonPropertyName("end_date")]
    public DateOnly? end_date { get; set; }

    [JsonPropertyName("type")]
    public string? type { get; set; }

    [JsonPropertyName("requested_at")]
    public DateTimeOffset? requested_at { get; set; }

    [JsonPropertyName("status")]
    public string? status { get; set; }

    [JsonPropertyName("vacation_type_id")]
    [JsonConverter(typeof(FlexibleNullableLongConverter))]
    public long? vacation_type_id { get; set; }
}

