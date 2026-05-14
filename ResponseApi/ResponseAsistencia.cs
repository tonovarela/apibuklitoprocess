using System;
using System.Text.Json.Serialization;

namespace apiBukLitoprocess.responseApi;

public class ResponseAsistencia
{
   [JsonPropertyName("pagination")]
    public required Pagination Pagination { get; set; }

    [JsonPropertyName("data")]
    public required List<AsistenciaRest> Data { get; set; }

}

public class AsistenciaRest
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    [JsonPropertyName("rut_trabajador")]
    public required string Rfc { get; set; }

    [JsonPropertyName("entrada_format")]
    public required string Entrada { get; set; }  

    [JsonPropertyName("salida_format")]
    public required string Salida { get; set; }  

    [JsonPropertyName("codigo_turno")]
    public required string Jornada { get; set; }

    [JsonPropertyName("turno")]
    public required string Turno { get; set; }

    
}
