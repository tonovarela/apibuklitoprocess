
using System.Text.Json.Serialization;

namespace apiBukLitoprocess.responseApi;

public class ResponseChecada
{

    [JsonPropertyName("pagination")]
    public required Pagination Pagination { get; set; }

    [JsonPropertyName("data")]
    public required List<ChecadaRest> Data { get; set; }
     
}

public partial class ChecadaRest
{
    
    [JsonPropertyName("DNI")]
    public required String RFC { get; set; }
    [JsonPropertyName("sentido")]
    public required String Tipo { get; set; }

    [JsonPropertyName("ano")]
    public required int Ano { get; set; }
    [JsonPropertyName("mes")]
    public required int Mes { get; set; }
    [JsonPropertyName("dia")]
    public required int Dia { get; set; }
    [JsonPropertyName("hora")]
    public required int Hora { get; set; }
    [JsonPropertyName("minutos")]
    public required int Minutos { get; set; }

    [JsonPropertyName("segundos")]
    public required int Segundos { get; set; }

    
}

