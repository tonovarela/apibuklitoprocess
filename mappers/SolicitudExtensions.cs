using System;
using System.Data.SqlTypes;
using apiBukLitoprocess.DTOs;
using apiBukLitoprocess.responseApi;

namespace apiBukLitoprocess.mappers;

public static class SolicitudExtensions
{

 public static bool PuedeMapearseASolicitud(this VacacionesRest vacaciones)
    {
        return  vacaciones.start_date.HasValue && vacaciones.end_date.HasValue;
    }

 public static SolicitudDTO toSolicitudDTO(this VacacionesRest vacaciones)
    {
        var fechaSolicitud = vacaciones.requested_at.HasValue
            ? NormalizarFechaSql(vacaciones.requested_at.Value.DateTime)
            : SqlDateTime.MinValue.Value;

        var fechaAutorizacion = vacaciones.approved_at.HasValue
            ? NormalizarFechaSql(vacaciones.approved_at.Value.DateTime)
            : SqlDateTime.MinValue.Value;

        var fechaInicio = NormalizarFechaSql(vacaciones.start_date!.Value.ToDateTime(new TimeOnly(0, 0)));
        var fechaFin = NormalizarFechaSql(vacaciones.end_date!.Value.ToDateTime(new TimeOnly(0, 0)));

        return new SolicitudDTO
        {
            id_solicitud = vacaciones.id,
            id_colaborador = vacaciones.employee_id,
            tipo = TipoSolicitud.VACACIONES,
            fechaAutorizacion = fechaAutorizacion,
            fechaSolicitud = fechaSolicitud,
            fechaInicio = fechaInicio,
            fechaFin = fechaFin,
            diasHabiles = Math.Max(vacaciones.working_days ?? 0, 0),
            id_autorizo = vacaciones.approved_by_id ?? 0
        };
    }

 private static DateTime NormalizarFechaSql(DateTime fecha)
    {
        if (fecha < SqlDateTime.MinValue.Value)
        {
            return SqlDateTime.MinValue.Value;
        }

        if (fecha > SqlDateTime.MaxValue.Value)
        {
            return SqlDateTime.MaxValue.Value;
        }

        return fecha;
    }

}
