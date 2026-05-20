
using apiBukLitoprocess.DTOs;
using apiBukLitoprocess.helpers;
using apiBukLitoprocess.responseApi;
namespace apiBukLitoprocess.mappers;

public  static class JornadaExtensions
{

    //ResponseJornada

    public static JornadaDTO toJornadaDTO( this ResponseJornada responseJornada )
    {

         string rawId = $"{responseJornada.DiaTurno}-{responseJornada.RFC}-{responseJornada.IdTurno}";
         var partesTurno = responseJornada.HorarioTurno.Split('-', 2, StringSplitOptions.TrimEntries);
         var inicio = partesTurno.Length == 2 ? partesTurno[0] : null;
          var fin = partesTurno.Length == 2 ? partesTurno[1] : null;
        return new JornadaDTO
        {
            Id_Jornada = HashGenerator.Generate(rawId),
            RFC = responseJornada.RFC,
            Fecha = DateTime.Parse(responseJornada.DiaTurno, new System.Globalization.CultureInfo("es-MX")).Date,
            Jornada = responseJornada.NombreTurno,
            Turno = responseJornada.HorarioTurno,
            Inicio = inicio,
            Fin = fin,
            Descanso = responseJornada.Vacaciones ? "Vacaciones" : responseJornada.Permiso ? "Permiso" : responseJornada.Licencia ? "Licencia" : "N/A"
        };
        
    }
   
}
