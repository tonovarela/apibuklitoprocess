
using apiBukLitoprocess.DTOs;
using apiBukLitoprocess.responseApi;

namespace apiBukLitoprocess.mappers
{
    public static class AsistenciaExtensions    
    {
        public static AsistenciaDTO ToAsistenciaDTO(this AsistenciaRest asistenciaRest)
        {        
            string[] partesTurno = asistenciaRest.Turno.Split('-', 2, StringSplitOptions.TrimEntries);
            var inicio = partesTurno.Length == 2 ? partesTurno[0] : null;
            var fin = partesTurno.Length == 2 ? partesTurno[1] : null;

            return new AsistenciaDTO
            {
                id_asistencia= asistenciaRest.Id,
                rfc= asistenciaRest.Rfc,                
                dia = DateTime.Parse(asistenciaRest.Dia),
                turno_noche = asistenciaRest.Turno.ToLower().Contains("noche"),
                turno = asistenciaRest.Turno,
                entrada=inicio ?? "",
                salida=fin ?? "",
                codigo_turno = asistenciaRest.CodigoTurno
            };
        }
        
    }
     
}