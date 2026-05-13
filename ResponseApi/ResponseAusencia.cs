
using System.Text.Json.Serialization;

namespace apiBukLitoprocess.responseApi;

public class ResponseAusencia
{

    [JsonPropertyName("pagination")]
    public required Pagination Pagination { get; set; }
    
    [JsonPropertyName("data")]
    public required AusenciaRest[] Data { get; set; }

}

public partial class AusenciaRest
{
    
    [JsonPropertyName("id")]
    public required long Id { get; set; }

    [JsonPropertyName("start_date")]
    public required DateOnly Fecha_inicio { get; set; }

    [JsonPropertyName("end_date")]
    public required DateOnly Fecha_fin { get; set; }

    [JsonPropertyName("status")]
    public required string Estado { get; set; }

    [JsonPropertyName("justification")]
    public required string Justificacion { get; set; }

    [JsonPropertyName("employee_id")]
    public required long EmployeeId { get; set; }

    [JsonPropertyName("start_time")]
    public  string? Inicio { get; set; }

    [JsonPropertyName("end_time")]
    public  string? Fin { get; set; }

     [JsonPropertyName("permission_type_code")]
    public required string Tipo { get; set; }

}
