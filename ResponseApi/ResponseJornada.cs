

using System.Text.Json.Serialization;

namespace apiBukLitoprocess.responseApi
{
    public class ResponseJornada
    {
         [JsonPropertyName("diaTurno")]
        public required string DiaTurno { get; set; }

        [JsonPropertyName("horarioTurno")]
        public required string HorarioTurno { get; set; }

        [JsonPropertyName("nombreTurno")]
        public required string NombreTurno { get; set; }

        [JsonPropertyName("idTurno")]
        public required string IdTurno { get; set; }

        [JsonPropertyName("tipoTurno")]
        public required string TipoTurno { get; set; }


        [JsonPropertyName("dni")]
        public required string RFC { get; set; }

        [JsonPropertyName("permiso")]
        public required bool Permiso { get; set; }
        [JsonPropertyName("vacaciones")]
        public required bool Vacaciones { get; set; }

        [JsonPropertyName("licencia")]
        public required bool Licencia { get; set; }
        
    }
}