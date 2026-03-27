using System;
using System.Text.Json.Serialization;

namespace apiBukLitoprocess.responseApi;

    public partial class ResponseAsistencia
    {
        [JsonPropertyName("pagination")]
        public required PaginationAsistencia Pagination { get; set; }

        [JsonPropertyName("data")]
        public required List<AsistenciaRest> Data { get; set; }
    }

    public partial class AsistenciaRest
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("trab_id")]
        public long TrabId { get; set; }

        [JsonPropertyName("rut_trabajador")]
        public string? RutTrabajador { get; set; }

        [JsonPropertyName("nombre")]
        public required string Nombre { get; set; }

        [JsonPropertyName("apellido_materno")]
        public required string ApellidoMaterno { get; set; }

        [JsonPropertyName("apellido_paterno")]
        public required string ApellidoPaterno { get; set; }

        [JsonPropertyName("id_recinto")]
        public long IdRecinto { get; set; }

        [JsonPropertyName("nombre_recinto")]
        public  string? NombreRecinto { get; set; }

        [JsonPropertyName("codigo_recinto")]
        public required string CodigoRecinto { get; set; }

        [JsonPropertyName("rut_empleador")]
        public string? RutEmpleador { get; set; }

        [JsonPropertyName("especialidad")]
        public string? Especialidad { get; set; }

        [JsonPropertyName("area")]
        public string? Area { get; set; }

        [JsonPropertyName("contrato")]
        public string? Contrato { get; set; }

        [JsonPropertyName("supervisor")]
        public string? Supervisor { get; set; }

        [JsonPropertyName("entrada")]
        public DateTimeOffset Entrada { get; set; }

        [JsonPropertyName("turno_noche")]
        public bool TurnoNoche { get; set; }

        [JsonPropertyName("salida")]
        public DateTimeOffset? Salida { get; set; }

        [JsonPropertyName("entrada_turno")]
        public DateTimeOffset EntradaTurno { get; set; }

        [JsonPropertyName("salida_turno")]
        public DateTimeOffset SalidaTurno { get; set; }

        [JsonPropertyName("dia_entrada")]
        public string? DiaEntrada { get; set; }

        [JsonPropertyName("entrada_format")]
        public  string? EntradaFormat { get; set; }

        [JsonPropertyName("salida_format")]
        public string? SalidaFormat { get; set; }

        [JsonPropertyName("art22")]
        public bool Art22 { get; set; }

        [JsonPropertyName("turno")]
        public required string Turno { get; set; }

        [JsonPropertyName("codigo_turno")]
        public required string CodigoTurno { get; set; }
    }

    public partial class PaginationAsistencia
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
