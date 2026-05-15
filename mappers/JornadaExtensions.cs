
using apiBukLitoprocess.DTOs;
using apiBukLitoprocess.helpers;
using apiBukLitoprocess.responseApi;
namespace apiBukLitoprocess.mappers;

public  static class JornadaExtensions
{
    public static JornadaDTO toJornadaDTO( this AsistenciaRest asistenciaRest )
    {

        string rawId = $"{asistenciaRest.Rfc}-{asistenciaRest.Entrada}-{DateTime.Parse(asistenciaRest.Entrada).ToString("yyyyMMdd")}";
        return new JornadaDTO
        {
            Id_Jornada = HashGenerator.Generate(rawId),
            RFC = asistenciaRest.Rfc,
            Fecha = DateTime.Parse(asistenciaRest.Entrada).Date,
            Jornada = asistenciaRest.Jornada,
            Turno = asistenciaRest.Turno
        };
    }

}
